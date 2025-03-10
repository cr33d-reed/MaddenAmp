/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2005 Colin Goudie
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; either version 2
 * of the License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 *
 * http://maddenamp.sourceforge.net/
 * 
 * maddeneditor@tributech.com.au
 * 
 *****************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using MaddenEditor.Core;
using MaddenEditor.Core.Record;

namespace MaddenEditor.Forms
{
	public partial class DepthChartEditorControl : UserControl, IEditorForm
	{
		private bool isInitialising = false;
		private EditorModel model = null;
		private DepthChartEditingModel depthEditingModel = null;
        public Overall playeroverall = new Overall();

		public DepthChartEditorControl()
		{
			isInitialising = true;
			InitializeComponent();
			isInitialising = false;
		}

		#region IEditorForm Members

		public MaddenEditor.Core.EditorModel Model
		{
			set { this.model = value;  }
		}

        public void InitialiseUI()
        {
            depthEditingModel = new DepthChartEditingModel(model);
            isInitialising = true;
            foreach (TeamRecord team in model.TeamModel.GetTeams())
            {
                teamCombo.Items.Add(team);
            }

            if (model.MadVersion < MaddenFileVersion.Ver2019)
            {
                for (int p = 0; p < 26; p++)
                {
                    string pos = Enum.GetName(typeof(MaddenPositions), p);
                    positionCombo.Items.Add(pos);
                }
            }
            else
            {
                for (int p = 0; p < 33; p++)
                {
                    string pos = Enum.GetName(typeof(MaddenPositions2019), p);
                    positionCombo.Items.Add(pos);
                }
                
                playeroverall.InitRatings19();
            }
            
            
            positionCombo.Text = positionCombo.Items[0].ToString();
            teamCombo.SelectedIndex = 0;

            isInitialising = false;

            LoadDepthChart();

            if (availablePlayerDatagrid.Rows.Count > 0)
                availablePlayerDatagrid.Rows[0].Selected = true;
            if (depthChartDataGrid.Rows.Count > 0)
                this.depthChartDataGrid.Rows[0].Selected = true;
        }

		public void CleanUI()
		{
			teamCombo.Items.Clear();
		}

		#endregion

		/// <summary>
		/// This function should realy be in the DepthChartEditingModel
		/// </summary>
		private void LoadDepthChart()
		{
			this.Cursor = Cursors.WaitCursor;
			isInitialising = true;
			int teamId = ((TeamRecord)teamCombo.SelectedItem).TeamId;
			int positionId = positionCombo.SelectedIndex;

			SortedList<int, DepthPlayerValueObject> depthList = depthEditingModel.GetPlayers(teamId, positionId);
			List<PlayerRecord> teamPlayers = depthEditingModel.GetAllPlayersOnTeamByOvr(teamId, positionId);

			depthChartDataGrid.Rows.Clear();

			foreach (DepthPlayerValueObject valObject in depthList.Values)
			{
                double overall = valObject.playerObject.Overall;
                if (valObject.playerObject.PositionId != positionCombo.SelectedIndex)
                    overall = playeroverall.GetOverall19(valObject.playerObject, positionCombo.SelectedIndex, -1);
                DataGridViewRow row = valObject.playerObject.GetDataRow(positionId, (int)overall);                
				//Now add our DepthChartRecord row on
				DataGridViewTextBoxCell depthCell = new DataGridViewTextBoxCell();
				depthCell.Value = valObject.depthObject;
				row.Cells.Add(depthCell);

				depthChartDataGrid.Rows.Add(row);
			}
			//Add the number of blank rows required for appropriate positions
			AddBlankRowsRequired(positionId);
			
			//Load the team's players in the availableDataGrid
			availablePlayerDatagrid.Rows.Clear();
			foreach (PlayerRecord record in teamPlayers)
			{
				// sting68 ok, for madden 19 we need to get overall separate from the normal function
                // for type we are going to have to use the players type, which isnt going to be accurate for
                // determining an overall for a player out of his normal position
                double overall = playeroverall.GetOverall19(record, positionId, -1);
                if (model.MadVersion == MaddenFileVersion.Ver2019)
                    availablePlayerDatagrid.Rows.Add(record.GetDataRow(positionId, (int)overall));
                else availablePlayerDatagrid.Rows.Add(record.GetDataRow(positionId,-1));
			}

            //Sort by OVR
            availablePlayerDatagrid.Sort(availablePlayerDatagrid.Columns["TeamOverall"], ListSortDirection.Descending);            

			teamDepthChartLabel.Text = teamCombo.Text + " Depth Chart (" + positionCombo.Text + ")";

			if (availablePlayerDatagrid.Rows.Count > 0)
			{
				if (availablePlayerDatagrid.SelectedRows.Count == 0)
					availablePlayerDatagrid.Rows[0].Selected = true;
			}
			if (depthChartDataGrid.Rows.Count > 0)
			{
				if (depthChartDataGrid.SelectedRows.Count == 0)
					this.depthChartDataGrid.Rows[0].Selected = true;
			}
			this.Cursor = Cursors.Default;
			isInitialising = false;
		}

		private void AddBlankRowsRequired(int positionid)
		{
			int currentCount = depthChartDataGrid.Rows.Count;
			int requiredCount = 3;
			switch (positionid)
			{
				case (int)MaddenPositions.QB:
				case (int)MaddenPositions.FB:
				case (int)MaddenPositions.TE:
				case (int)MaddenPositions.LT:
				case (int)MaddenPositions.LG:
				case (int)MaddenPositions.C:
				case (int)MaddenPositions.RG:
				case (int)MaddenPositions.RT:
				case (int)MaddenPositions.LE:
				case (int)MaddenPositions.RE:
				case (int)MaddenPositions.LOLB:
				case (int)MaddenPositions.ROLB:
				case (int)MaddenPositions.FS:
				case (int)MaddenPositions.SS:
				case (int)MaddenPositions.K:
				case (int)MaddenPositions.P:
				case 23: // KOS
				case 24: // LS
				case 25: // 3DRB
                case 27:
                case 28:
                case 29:
                case 30:
                case 31:
                case 32:
                case 33:
					//Must have 3 positions
					requiredCount = 3;
					break;
				case (int)MaddenPositions.HB:
				case (int)MaddenPositions.DT:
				case 21: // KR
				case 22: // PR
					//Must have 4 positions
					requiredCount = 4;
					break;
				case (int)MaddenPositions.WR:
					//Must have 6 positions
					requiredCount = 6;
					break;
				case (int)MaddenPositions.CB:
					//must have 5 positions
					requiredCount = 5;
					break;
                case 26:
                    requiredCount = 2;
                    break;



				default:
					break;
					
			}

			while (currentCount++ < requiredCount)
			{
				depthChartDataGrid.Rows.Add();
			}
		}

		private void teamCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				LoadDepthChart();
			}
		}

		private void positionCombo_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!isInitialising)
			{
				LoadDepthChart();
			}
		}

		private void availablePlayerDatagrid_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				availablePlayerDatagrid.ClearSelection();
				availablePlayerDatagrid.Rows[e.RowIndex].Selected = true;
			}
		}

		private void availablePlayerDatagrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				availablePlayerDatagrid.ClearSelection();
				availablePlayerDatagrid.Rows[e.RowIndex].Selected = true;
			}
		}

		private void depthChartDataGrid_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				depthChartDataGrid.ClearSelection();
				depthChartDataGrid.Rows[e.RowIndex].Selected = true;
			}
		}

		private void depthChartDataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0)
			{
				depthChartDataGrid.ClearSelection();
				depthChartDataGrid.Rows[e.RowIndex].Selected = true;
			}
		}

		private void depthOrderDownButton_Click(object sender, EventArgs e)
		{
			if (depthChartDataGrid.SelectedRows.Count == 1)
			{
				//We have to find the two DepthChart objects to exchange positions
				if (depthChartDataGrid.Rows.Count == 1)
				{
					//We can't transfer down if there are only 1 row 
					return;
				}
				if (depthChartDataGrid.SelectedRows[0].Index == depthChartDataGrid.Rows.Count - 1)
				{
					//We can't transfer the bottom row down
					return;
				}
				int selIndex = depthChartDataGrid.SelectedRows[0].Index;
				//There are at least 2 rows and we arent at the bottom so
				DepthChartRecord selectedRecord = (DepthChartRecord)depthChartDataGrid.Rows[depthChartDataGrid.SelectedRows[0].Index].Cells[4].Value;
				DepthChartRecord destinationRecord = (DepthChartRecord)depthChartDataGrid.Rows[depthChartDataGrid.SelectedRows[0].Index + 1].Cells[4].Value;

				//Now don't swap to/from a null row
				if (selectedRecord == null || destinationRecord == null)
				{
					return;
				}

				int tempDepthOrd = destinationRecord.DepthOrder;
				destinationRecord.DepthOrder = selectedRecord.DepthOrder;
				selectedRecord.DepthOrder = tempDepthOrd;

				LoadDepthChart();

				//Now select the one we just moved
				depthChartDataGrid.ClearSelection();
				depthChartDataGrid.Rows[selIndex + 1].Selected = true;
			}
		}

		private void depthOrderUpButton_Click(object sender, EventArgs e)
		{
			if (depthChartDataGrid.SelectedRows.Count == 1)
			{
				//We have to find the two DepthChart objects to exchange positions
				if (depthChartDataGrid.Rows.Count == 1)
				{
					//We can't transfer up if there are only 1 row 
					return;
				}
				if (depthChartDataGrid.SelectedRows[0].Index == 0)
				{
					//We can't transfer the top row up
					return;
				}
				int selIndex = depthChartDataGrid.SelectedRows[0].Index;
				//There are at least 2 rows and we arent at the top so
				DepthChartRecord selectedRecord = (DepthChartRecord)depthChartDataGrid.Rows[depthChartDataGrid.SelectedRows[0].Index].Cells[4].Value;
				DepthChartRecord destinationRecord = (DepthChartRecord)depthChartDataGrid.Rows[depthChartDataGrid.SelectedRows[0].Index-1].Cells[4].Value;

				//Now don't swap to/from a null row
				if (selectedRecord == null || destinationRecord == null)
				{
					return;
				}

				int tempDepthOrd = destinationRecord.DepthOrder;
				destinationRecord.DepthOrder = selectedRecord.DepthOrder;
				selectedRecord.DepthOrder = tempDepthOrd;

				LoadDepthChart();

				//Now select the one we just moved
				depthChartDataGrid.ClearSelection();
				depthChartDataGrid.Rows[selIndex - 1].Selected = true;
			}
		}

		private void transferButton_Click(object sender, EventArgs e)
		{
			if (availablePlayerDatagrid.SelectedRows.Count == 1 && depthChartDataGrid.SelectedRows.Count == 1)
			{
				//First check that the player we are bringing up into the depth chart isnt already there
				foreach (DataGridViewRow row in depthChartDataGrid.Rows)
				{
					if (row.Cells[3].Value == availablePlayerDatagrid.SelectedRows[0].Cells[3].Value)
					{
						//Trying to transfer a player already there
						return;
					}
				}
				if (depthChartDataGrid.SelectedRows[0].Cells[4].Value == null)
				{
					//We are transfering to a blank depthchart record we need to create a record for it
					DepthChartRecord newRecord = (DepthChartRecord)model.TableModels[EditorModel.DEPTH_CHART_TABLE].CreateNewRecord(true);
					PlayerRecord playerRecord = (PlayerRecord)availablePlayerDatagrid.SelectedRows[0].Cells[3].Value;

					newRecord.PlayerId = playerRecord.PlayerId;
					newRecord.TeamId = playerRecord.TeamId;
					newRecord.DepthOrder = depthChartDataGrid.SelectedRows[0].Index;
					newRecord.PositionId = positionCombo.SelectedIndex;

					LoadDepthChart();
					return;
				}
				else
				{
					//We are swapping two players
					DepthChartRecord depthRecord = (DepthChartRecord)depthChartDataGrid.SelectedRows[0].Cells[4].Value;

					PlayerRecord playerRecord = (PlayerRecord)availablePlayerDatagrid.SelectedRows[0].Cells[3].Value;

					depthRecord.PlayerId = playerRecord.PlayerId;

					//Load up the screen again
					LoadDepthChart();
					return;
				}
			}
		}

		private void eraseButton_Click(object sender, EventArgs e)
		{
			if (depthChartDataGrid.SelectedRows.Count == 1)
			{
				if (depthChartDataGrid.SelectedRows[0].Cells[4].Value == null)
				{
					//Don't delete an empy row
					return;
				}
				else
				{
					DepthChartRecord record = (DepthChartRecord)depthChartDataGrid.SelectedRows[0].Cells[4].Value;
					record.SetDeleteFlag(true);
					//move the other records up
					int index = record.DepthOrder + 1;
					while (index < depthChartDataGrid.Rows.Count)
					{
						if (depthChartDataGrid.Rows[index].Cells[4].Value == null)
						{
							//Found an empty
							break;
						}
						//move this one up
						((DepthChartRecord)depthChartDataGrid.Rows[index].Cells[4].Value).DepthOrder--;
						index++;
					}
					LoadDepthChart();
					return;
				}
			}
		}

		private void applyButton_Click(object sender, EventArgs e)
		{
			this.ParentForm.Close();
		}
	}
}
