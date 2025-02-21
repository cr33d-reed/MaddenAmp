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
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace MaddenEditor.Core.Record
{
    public enum Rating
    {
        STR = 0,
        AGI = 1,
        SPD = 2,
        ACC = 3,
        AWR = 4,
        CTH = 5,
        CAR = 6,
        THP = 7,
        THA = 8,
        KPW = 9,
        KAC = 10,
        BTK = 11,
        TAK = 12,
        PBK = 13,
        RBK = 14,
        JMP = 15,
        KRT = 16,
        IMP = 17,
        INJ = 18
    }
    
    public class PlayerRecord : TableRecordModel
	{
        
        
        // table name "PLAY"
        #region Record members
        public const string ACCELERATION = "PACC";
        public const string AGE = "PAGE";
        public const string AGILITY = "PAGI";
        public const string AWARENESS = "PAWR";
        public const string NASAL_STRIP = "PBRE";
        public const string BREAK_TACKLE = "PBTK";
        public const string CARRYING = "PCAR";
        public const string PCEL = "PCEL";                      // 2004-2006 celebration?  replaced by ego 2007-2008
        public const string EQP_PAD_SHELF = "PCHS";
        public const string PLAYER_COMMENT = "PCMT";
        public const string COLLEGE_ID = "PCOL";
        public const string CONTRACT_LENGTH = "PCON";
        public const string CAREER_PHASE = "PCPH";
        public const string SALARY_CURRENT = "PCSA";            // franchise only
        public const string CATCHING = "PCTH";
        public const string PCTS = "PCTS";                      // 2007-2008 ??
        public const string CONTRACT_YRS_LEFT = "PCYL";
        public const string DRAFT_ROUND_INDEX = "PDPI";
        public const string DRAFT_ROUND = "PDRO";
        public const string PLAYER_EGO = "PEGO";                // 2007-2008
        public const string EYE_PAINT = "PEYE";
        public const string ARMS_FAT = "PFAS";
        public const string LEGS_CALF_FAT = "PFCS";
        public const string FACE_ID = "PFEx";
        public const string FACE_SHAPE = "PFGE";                // 2004 field, 2005
        public const string PFGS = "PFGS";                      // ?
        public const string HOLDOUT = "PFHO";                   // ?
        public const string LEGS_THIGH_FAT = "PFHS";
        public const string FACE_MASK = "PFMK";
        public const string FIRST_NAME = "PFNA";
        public const string PRO_BOWL = "PFPB";
        public const string BODY_FAT = "PFTS";
        public const string PLAYER_ID = "PGID";
        public const string SLEEVES_A = "PGSL";
        public const string DOMINANT_HAND = "PHAN";
        public const string HAIR_COLOR = "PHCL";
        public const string HAIR_STYLE = "PHED";
        public const string HEIGHT = "PHGT";
        public const string HELMET_STYLE = "PHLM";
        public const string NFL_ICON = "PICN";
        public const string IMPORTANCE = "PIMP";
        public const string INJURY = "PINJ";
        public const string JERSEY_NUMBER = "PJEN";
        public const string JERSEY = "PJER";
        public const string JUMPING = "PJMP";
        public const string PLAYER_JERSEY_INITIALS = "PJTY";
        public const string KICK_ACCURACY = "PKAC";
        public const string KICK_POWER = "PKPR";
        public const string KICK_RETURN = "PKRT";
        public const string LEFT_ELBOW_A = "PLEL";
        public const string PLFH = "PLFH";                      // ?
        public const string LEFT_HAND_A = "PLHA";
        public const string LAST_HEALTHY_YEAR = "PLHY";         // not sure about this one
        public const string LAST_NAME = "PLNA";
        public const string PLPL = "PLPL";                      // progession related?
        public const string LEFT_SHOE = "PLSH";
        public const string EQP_SHOES = "PLSS";
        public const string LEFT_KNEE = "PLTH";
        public const string LEFT_WRIST_A = "PLWR";
        public const string ARMS_MUSCLE = "PMAS";
        public const string LEGS_CALF_MUSCLE = "PMCS";
        public const string REAR_FAT = "PMGS";
        public const string LEGS_THIGH_MUSCLE = "PMHS";
        public const string MORALE = "PMOR";
        public const string MOUTHPIECE = "PMPC";
        public const string BODY_WEIGHT = "PMTS";
        public const string MUSCLE = "PMUS";
        public const string NECK_ROLL = "PNEK";
        public const string NFL_ID = "POID";
        public const string ORIGINAL_POSITION_ID = "POPS";      // 2005+
        public const string OVERALL = "POVR";
        public const string PASS_BLOCKING = "PPBK";
        public const string PLAYED_GAMES = "PPGA";              // progression period games played
        public const string POSITION_ID = "PPOS";
        public const string PPSP = "PPSP";                      // progression related?
        public const string PREVIOUS_TEAM_ID = "PPTI";
        public const string REAR_SHAPE = "PQGS";
        public const string EQP_FLAK_JACKET = "PQTS";
        public const string RUN_BLOCKING = "PRBK";
        public const string RIGHT_ELBOW_A = "PREL";
        public const string RIGHT_HAND_A = "PRHA";
        public const string PLAYER_ROLE = "PROL";               // 2007
        public const string PLAYER_WEAPON = "PRL2";             // 2008
        public const string RIGHT_SHOE = "PRSH";
        public const string RIGHT_KNEE = "PRTH";
        public const string RIGHT_WRIST_A = "PRWR";
        public const string SALARY_YEAR_0 = "PSA0";             
        public const string SALARY_YEAR_1 = "PSA1";
        public const string SALARY_YEAR_2 = "PSA2";
        public const string SALARY_YEAR_3 = "PSA3";
        public const string SALARY_YEAR_4 = "PSA4";
        public const string SALARY_YEAR_5 = "PSA5";
        public const string SALARY_YEAR_6 = "PSA6";
        public const string SIGNING_BONUS_YEAR_0 = "PSB0";
        public const string SIGNING_BONUS_YEAR_1 = "PSB1";      
        public const string SIGNING_BONUS_YEAR_2 = "PSB2";
        public const string SIGNING_BONUS_YEAR_3 = "PSB3";
        public const string SIGNING_BONUS_YEAR_4 = "PSB4";
        public const string SIGNING_BONUS_YEAR_5 = "PSB5";
        public const string SIGNING_BONUS_YEAR_6 = "PSB6";
        public const string SIGNING_BONUS_TOTAL = "PSBO";
        public const string BODY_OVERALL = "PSBS";
        public const string PSKI = "PSKI";                      // ?
        public const string SPEED = "PSPD";
        public const string STAMINA = "PSTA";
        public const string PSTM = "PSTM";                      // ?
        public const string STRENGTH = "PSTR";
        public const string THROWING_STYLE = "PSTY";
        public const string PORTRAIT_ID = "PSXP";
        public const string TACKLE = "PTAK";
        public const string LEFT_TATTOO = "PTAL";
        public const string RIGHT_TATTOO = "PTAR";
        public const string TENDENCY = "PTEN";
        public const string TOUGHNESS = "PTGH";
        public const string THROW_ACCURACY = "PTHA";
        public const string THROW_POWER = "PTHP";
        public const string LEGS_THIGH_PADS = "PTPS";           // 2004-2005
        public const string TOTAL_SALARY = "PTSA";              
        public const string SLEEVES_B = "PTSL";
        public const string EQP_PAD_HEIGHT = "PTSS";
        public const string PUCL = "PUCL";                      // ?
        public const string BODY_MUSCLE = "PUTS";
        public const string PLAYER_VALUE = "PVAL";              // 2008, this is 0-7 values
        public const string PVCO = "PVCO";                      // previous contract length?
        public const string VISOR = "PVIS";
        public const string PREVIOUS_SIGNING_BONUS_TOTAL = "PVSB";
        public const string PREVIOUS_TOTAL_SALARY = "PVTS";
        public const string WEIGHT = "PWGT";
        public const string PWIN = "PWIN";
        public const string EQP_PAD_WIDTH = "PWSS";
        public const string YRS_PRO = "PYRP";
        public const string YEARS_WITH_TEAM = "PYWT";
		public const string TEAM_ID = "TGID";
        public const string LEFT_ELBOW_B = "TLEL"; 
        public const string LEFT_HAND_B = "TLHA";
        public const string LEFT_WRIST_B = "TLWR";
        public const string RIGHT_ELBOW_B = "TREL";
		public const string RIGHT_HAND_B = "TRHA";
        public const string RIGHT_WRIST_B = "TRWR";
        #endregion

        public PlayerRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        private bool calculatedCapHit = false;
        private int capHit = 0;
        private int capHitDifference = 0;
        double[] estYearlySalary = new double[7];
        double[] estSigningBonusArray = new double[7];

        public bool Starter = false;
        public int backupValue = 0;
		public override string ToString()
		{
			return FirstName + " " + LastName + " (" + Enum.GetNames(typeof(MaddenPositions))[PositionId].ToString() + ")";
		}

        #region Get/SET

        public string FirstName
		{
			get
			{
				//The first time we access this record we should calculate this players cap hit
                //this is wrong...fix
				//if (!calculatedCapHit)
				//{
				//	CalculateCapHit(false);
				//}
				return GetStringField(FIRST_NAME);
			}
			set
			{
				SetField(FIRST_NAME, value);
			}
		}

		public string LastName
		{
			get
			{
				return GetStringField(LAST_NAME);
			}
			set
			{
				SetField(LAST_NAME, value);
			}
		}

		public int PositionId
		{
			get
			{
				return GetIntField(POSITION_ID);
			}
			set
			{
				SetField(POSITION_ID, value);
			}
		}

		public int TeamId
		{
			get
			{
				return GetIntField(TEAM_ID);
			}
			set
			{
				SetField(TEAM_ID, value);
			}
		}

        public int PreviousTeamId
        {
            get { return GetIntField(PREVIOUS_TEAM_ID); }
            set { SetField(PREVIOUS_TEAM_ID, value); }
        }
        
        public int PlayerId
		{
			get
			{
				return GetIntField(PLAYER_ID);
			}
			set
			{
				SetField(PLAYER_ID, value);
			}
		}

        public int NFLID
        {
            get
            {
                return GetIntField(NFL_ID);
            }
            set
            {
                SetField(NFL_ID, value);
            }
        }

		public int CollegeId
		{
			get
			{
				return GetIntField(COLLEGE_ID);
			}
			set
			{
				SetField(COLLEGE_ID, value);
			}
		}

		public int Age
		{
			get
			{
				return GetIntField(AGE);
			}
			set
			{
				SetField(AGE, value);
			}
		}

		public int YearsPro
		{
			get
			{
				return GetIntField(YRS_PRO);
			}
			set
			{
				SetField(YRS_PRO, value);
			}
		}

		public int PortraitId
		{
			get
			{
				return GetIntField(PORTRAIT_ID);
			}
			set
			{
				SetField(PORTRAIT_ID, value);
			}
		}

		public bool NFLIcon
		{
			get
			{
				return (GetIntField(NFL_ICON) == 1);
			}
			set
			{
				SetField(NFL_ICON, Convert.ToInt32(value));
			}
		}

		public bool ProBowl
		{
			get
			{
				return (GetIntField(PRO_BOWL) == 1);
			}
			set
			{
				SetField(PRO_BOWL, Convert.ToInt32(value));
			}
		}

		public bool DominantHand
		{
			get
			{
				return (GetIntField(DOMINANT_HAND) == 1);
			}
			set
			{
				SetField(DOMINANT_HAND, Convert.ToInt32(value));
			}
		}

		public int JerseyNumber
		{
			get
			{
				return GetIntField(JERSEY_NUMBER);
			}
			set
			{
				SetField(JERSEY_NUMBER, value);
			}
		}

		public int Overall
		{
			get
			{
				return GetIntField(OVERALL);
			}
			set
			{
				SetField(OVERALL, value);
			}
		}

		public int Speed
		{
			get
			{
				return GetIntField(SPEED);
			}
			set
			{
				SetField(SPEED, value);
			}
		}

		public int Strength
		{
			get
			{
				return GetIntField(STRENGTH);
			}
			set
			{
				SetField(STRENGTH, value);
			}
		}

		public int Awareness
		{
			get
			{
				return GetIntField(AWARENESS);
			}
			set
			{
				SetField(AWARENESS, value);
			}
		}

		public int Agility
		{
			get
			{
				return GetIntField(AGILITY);
			}
			set
			{
				SetField(AGILITY, value);
			}
		}

		public int Acceleration
		{
			get
			{
				return GetIntField(ACCELERATION);
			}
			set
			{
				SetField(ACCELERATION, value);
			}
		}

		public int Catching
		{
			get
			{
				return GetIntField(CATCHING);
			}
			set
			{
				SetField(CATCHING, value);
			}
		}

		public int Carrying
		{
			get
			{
				return GetIntField(CARRYING);
			}
			set
			{
				SetField(CARRYING, value);
			}
		}

		public int Jumping
		{
			get
			{
				return GetIntField(JUMPING);
			}
			set
			{
				SetField(JUMPING, value);
			}
		}

		public int BreakTackle
		{
			get
			{
				return GetIntField(BREAK_TACKLE);
			}
			set
			{
				SetField(BREAK_TACKLE, value);
			}
		}

		public int Tackle
		{
			get
			{
				return GetIntField(TACKLE);
			}
			set
			{
				SetField(TACKLE, value);
			}
		}

		public int ThrowPower
		{
			get
			{
				return GetIntField(THROW_POWER);
			}
			set
			{
				SetField(THROW_POWER, value);
			}
		}

		public int ThrowAccuracy
		{
			get
			{
				return GetIntField(THROW_ACCURACY);
			}
			set
			{
				SetField(THROW_ACCURACY, value);
			}
		}

		public int PassBlocking
		{
			get
			{
				return GetIntField(PASS_BLOCKING);
			}
			set
			{
				SetField(PASS_BLOCKING, value);
			}
		}

		public int RunBlocking
		{
			get
			{
				return GetIntField(RUN_BLOCKING);
			}
			set
			{
				SetField(RUN_BLOCKING, value);
			}
		}

		public int KickPower
		{
			get
			{
				return GetIntField(KICK_POWER);
			}
			set
			{
				SetField(KICK_POWER, value);
			}
		}

		public int KickAccuracy
		{
			get
			{
				return GetIntField(KICK_ACCURACY);
			}
			set
			{
				SetField(KICK_ACCURACY, value);
			}
		}

		public int KickReturn
		{
			get
			{
				return GetIntField(KICK_RETURN);
			}
			set
			{
				SetField(KICK_RETURN, value);
			}
		}

		public int Stamina
		{
			get
			{
				return GetIntField(STAMINA);
			}
			set
			{
				SetField(STAMINA, value);
			}
		}

		public int Injury
		{
			get
			{
				return GetIntField(INJURY);
			}
			set
			{
				SetField(INJURY, value);
			}
		}

		public int Toughness
		{
			get
			{
				return GetIntField(TOUGHNESS);
			}
			set
			{
				SetField(TOUGHNESS, value);
			}
		}

		public int Morale
		{
			get
			{
				return GetIntField(MORALE);
			}
			set
			{
				SetField(MORALE, value);
			}
		}

		public int Importance
		{
			get
			{
				return GetIntField(IMPORTANCE);
			}
			set
			{
				SetField(IMPORTANCE, value);
			}
		}

		public int Weight
		{
			get
			{
				return GetIntField(WEIGHT);
			}
			set
			{
				SetField(WEIGHT, value);
			}
		}

		public int Height
		{
			get
			{
				return GetIntField(HEIGHT);
			}
			set
			{
				SetField(HEIGHT, value);
			}
		}

		public int BodyWeight
		{
			get
			{
				return (GetIntField(BODY_WEIGHT) < 100 ? GetIntField(BODY_WEIGHT) : 99);
			}
			set
			{
				SetField(BODY_WEIGHT, value);
			}
		}
        
        public int ContractLength
		{
			get
			{
				return GetIntField(CONTRACT_LENGTH);
			}
			set
			{
				SetField(CONTRACT_LENGTH, value);
			}
		}

		public int ContractYearsLeft
		{
			get
			{
				return GetIntField(CONTRACT_YRS_LEFT);
			}
			set
			{
				SetField(CONTRACT_YRS_LEFT, value);
			}
		}

		public int PreviousSigningBonus
		{
			get
			{
				return GetIntField(PREVIOUS_SIGNING_BONUS_TOTAL);
			}
			set
			{
				SetField(PREVIOUS_SIGNING_BONUS_TOTAL, value);
			}
		}

        public int CurrentSalary
        {
            get { return GetIntField(SALARY_CURRENT); }
            set { SetField(SALARY_CURRENT, value); }        
        }
        	
		public int FaceId
		{
			get
			{
				return GetIntField(FACE_ID);
			}
			set
			{
				SetField(FACE_ID, value);
			}
		}
        		
		public bool SideArmed
		{
			get	{ return GetIntField(THROWING_STYLE) == 1; }
            set { SetField(THROWING_STYLE, Convert.ToInt32(value)); }
		}
        
		public int Tendency
		{
			get
			{
				return GetIntField(TENDENCY);
			}
			set
			{
				SetField(TENDENCY, value);
			}
		}

		public int DraftRoundIndex
		{
			get
			{
				return GetIntField(DRAFT_ROUND_INDEX);
			}
			set
			{
				SetField(DRAFT_ROUND_INDEX, value);
			}
		}

		public int DraftRound
		{
			get
			{
				return GetIntField(DRAFT_ROUND);
			}
			set
			{
				SetField(DRAFT_ROUND, value);
			}
		}
        
        public int PlayerComment
        {
            get { return GetIntField(PLAYER_COMMENT); }
            set { SetField(PLAYER_COMMENT, value); }
        }

        public int JerseyInitials
        {
            get { return GetIntField(PLAYER_JERSEY_INITIALS); }
            set { SetField(PLAYER_JERSEY_INITIALS, value); }
        }

        public int TotalSalary
        {
            get { return GetIntField(TOTAL_SALARY); }
            set { SetField(TOTAL_SALARY, value); }
        }

        public int OriginalPositionId
        {
            get { return GetIntField(ORIGINAL_POSITION_ID); }
            set { SetField(ORIGINAL_POSITION_ID, value); }
        }

        public int Pcel
        {
            get { return GetIntField(PCEL); }
            set { SetField(PCEL, value); }
        }

        public int Pfgs

        {
            get { return GetIntField(PFGS); }
            set { SetField(PFGS, value); }
        }

        public bool Holdout
        {
            get { return GetIntField(HOLDOUT) == 1; }
            set { SetField(HOLDOUT, Convert.ToInt32(value)); }
        }

        public int Pcts
        {
            get { return GetIntField(PCTS); }
            set { SetField(PCTS, value); }
        }

        public bool Jersey
        {
            get { return GetIntField(JERSEY) ==1; }
            set { SetField(JERSEY, Convert.ToInt32(value)); }
        }

        public bool Plfh
        {
            get { return GetIntField(PLFH)==1; }
            set { SetField(PLFH, Convert.ToInt32(value)); }
        }

        public int LastHealthy
        {
            get { return GetIntField(LAST_HEALTHY_YEAR); }
            set { SetField(LAST_HEALTHY_YEAR, value); }
        }

        public int Plpl
        {
            get { return GetIntField(PLPL); }
            set { SetField(PLPL, value); }
        }

        public int Ppsp
        {
            get { return GetIntField(PPSP); }
            set { SetField(PPSP, value); }
        }

        public int PlayedGames
        {
            get { return GetIntField(PLAYED_GAMES); }
            set { SetField(PLAYED_GAMES, value); }
        }
        
        public int SigningBonus
        {
            get { return GetIntField(SIGNING_BONUS_TOTAL); }
            set 
            { 
                SetField(SIGNING_BONUS_TOTAL, value);
                CalculateCapHit(true);
            }
        }

        public int Pski
        {
            get { return GetIntField(PSKI); }
            set { SetField(PSKI, value); }
        }

        public int Pstm
        {
            get { return GetIntField(PSTM); }
            set { SetField(PSTM, value); }
        }
        
        public int Pucl
        {
            get { return GetIntField(PUCL); }
            set { SetField(PUCL, value); }
        }

        public int Pvco
        {
            get { return GetIntField(PVCO); }
            set { SetField(PVCO, value); }
        }


        public int Salary0
        {
            get { return GetIntField(SALARY_YEAR_0); }
            set { SetField(SALARY_YEAR_0, value);
            FixCurrentSalary();
            }
        }
        public int Salary1
        {
            get { return GetIntField(SALARY_YEAR_1); }
            set { SetField(SALARY_YEAR_1, value);
            FixCurrentSalary();
            }
        }
        public int Salary2
        {
            get { return GetIntField(SALARY_YEAR_2); }
            set { SetField(SALARY_YEAR_2, value);
            FixCurrentSalary();
            }
        }
        public int Salary3
        {
            get { return GetIntField(SALARY_YEAR_3); }
            set { SetField(SALARY_YEAR_3, value);
            FixCurrentSalary();
            }
        }
        public int Salary4
        {
            get { return GetIntField(SALARY_YEAR_4); }
            set { SetField(SALARY_YEAR_4, value);
            FixCurrentSalary();
            }
        }
        public int Salary5
        {
            get { return GetIntField(SALARY_YEAR_5); }
            set { SetField(SALARY_YEAR_5, value); 
                FixCurrentSalary(); }
        }
        public int Salary6
        {
            get { return GetIntField(SALARY_YEAR_6); }
            set { SetField(SALARY_YEAR_6, value);
            FixCurrentSalary();
            }
        }
        public int Bonus0
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_0); }
            set { SetField(SIGNING_BONUS_YEAR_0, value);
            FixCurrentSalary(); }
        }
        public int Bonus1
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_1); }
            set { SetField(SIGNING_BONUS_YEAR_1, value);
            FixCurrentSalary();
            }
        }
        public int Bonus2
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_2); }
            set { SetField(SIGNING_BONUS_YEAR_2, value);
            FixCurrentSalary();
            }
        }
        public int Bonus3
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_3); }
            set { SetField(SIGNING_BONUS_YEAR_3, value);
            FixCurrentSalary();
            }
        }
        public int Bonus4
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_4); }
            set { SetField(SIGNING_BONUS_YEAR_4, value);
            FixCurrentSalary();
            }
        }
        public int Bonus5
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_5); }
            set { SetField(SIGNING_BONUS_YEAR_5, value);
            FixCurrentSalary();
            }
        }
        public int Bonus6
        {
            get { return GetIntField(SIGNING_BONUS_YEAR_6); }
            set { SetField(SIGNING_BONUS_YEAR_6, value);
            FixCurrentSalary();
            }
        }

        public int BonusTotal
        {
            get { return GetIntField(SIGNING_BONUS_TOTAL); }
            set
            {
                SetField(SIGNING_BONUS_TOTAL, value);
                FixCurrentSalary();
            }
        }

        #region Appearance / Equipment
        public bool RightKnee
        {
            get { return GetIntField(RIGHT_KNEE) == 1; }
            set { SetField(RIGHT_KNEE, Convert.ToInt32(value)); }
        }

        public int LeftElbow
        {
            get
            {
                return GetIntField(LEFT_ELBOW_A);
            }
            set
            {
                SetField(LEFT_ELBOW_A, value);
                SetField(LEFT_ELBOW_B, value);
            }
        }

        public int RightElbow
        {
            get
            {
                return GetIntField(RIGHT_ELBOW_A);
            }
            set
            {
                SetField(RIGHT_ELBOW_A, value);
                SetField(RIGHT_ELBOW_B, value);
            }
        }

        public int Sleeves
        {
            get
            {
                return GetIntField(SLEEVES_A);
            }
            set
            {
                SetField(SLEEVES_A, value);
                SetField(SLEEVES_B, value);
            }
        }

        public int LeftWrist
        {
            get
            {
                return GetIntField(LEFT_WRIST_A);
            }
            set
            {
                SetField(LEFT_WRIST_A, value);
                SetField(LEFT_WRIST_B, value);
            }
        }

        public int RightWrist
        {
            get
            {
                return GetIntField(RIGHT_WRIST_A);
            }
            set
            {
                SetField(RIGHT_WRIST_A, value);
                SetField(RIGHT_WRIST_B, value);
            }
        }

        public int NasalStrip
        {
            get
            {
                return GetIntField(NASAL_STRIP);
            }
            set
            {
                SetField(NASAL_STRIP, value);
            }
        }

        public int LeftTattoo
        {
            get
            {
                return GetIntField(LEFT_TATTOO);
            }
            set
            {
                SetField(LEFT_TATTOO, value);
            }
        }

        public int RightTattoo
        {
            get
            {
                return GetIntField(RIGHT_TATTOO);
            }
            set
            {
                SetField(RIGHT_TATTOO, value);
            }
        }

        public int BodyOverall
        {
            get
            {
                return 99 - (GetIntField(BODY_OVERALL) > 99 ? 99 : GetIntField(BODY_OVERALL));
            }
            set
            {
                SetField(BODY_OVERALL, 99 - value);
            }
        }

        public int LegsThighPads
        {
            get
            {
                return 99 - (GetIntField(LEGS_THIGH_PADS) > 99 ? 99 : GetIntField(LEGS_THIGH_PADS));
            }
            set
            {
                SetField(LEGS_THIGH_PADS, 99 - value);
            }
        }

        public int HairColor
        {
            get
            {
                return GetIntField(HAIR_COLOR);
            }
            set
            {
                SetField(HAIR_COLOR, value);
            }
        }

        public int HairStyle
        {
            get
            {
                return GetIntField(HAIR_STYLE);
            }
            set
            {
                SetField(HAIR_STYLE, value);
            }
        }

        public int HelmetStyle
        {
            get
            {
                return GetIntField(HELMET_STYLE);
            }
            set
            {
                SetField(HELMET_STYLE, value);
            }
        }

        public int FaceMask
        {
            get
            {
                return GetIntField(FACE_MASK);
            }
            set
            {
                SetField(FACE_MASK, value);
            }
        }

        public int NeckRoll
        {
            get
            {
                return (GetIntField(NECK_ROLL) < 3 ? GetIntField(NECK_ROLL) : 2);
            }
            set
            {
                SetField(NECK_ROLL, value);
            }
        }

        public int Visor
        {
            get
            {
                return GetIntField(VISOR);
            }
            set
            {
                SetField(VISOR, value);
            }
        }

        public int MouthPiece
        {
            get
            {
                return GetIntField(MOUTHPIECE);
            }
            set
            {
                SetField(MOUTHPIECE, value);
            }
        }

        public int LeftHand
        {
            get
            {
                return GetIntField(LEFT_HAND_A);
            }
            set
            {
                SetField(LEFT_HAND_A, value);
                SetField(LEFT_HAND_B, value);
            }
        }

        public int RightHand
        {
            get
            {
                return GetIntField(RIGHT_HAND_A);
            }
            set
            {
                SetField(RIGHT_HAND_A, value);
                SetField(RIGHT_HAND_B, value);
            }
        }

        public int LeftShoe
        {
            get
            {
                return GetIntField(LEFT_SHOE);
            }
            set
            {
                SetField(LEFT_SHOE, value);
            }
        }

        public int RightShoe
        {
            get
            {
                return GetIntField(RIGHT_SHOE);
            }
            set
            {
                SetField(RIGHT_SHOE, value);
            }
        }

        public bool LeftKnee
        {
            get { return GetIntField(LEFT_KNEE) == 1; }
            set { SetField(LEFT_KNEE, Convert.ToInt32(value)); }
        }

        public int BodyMuscle
        {
            get
            {
                return (GetIntField(BODY_MUSCLE) < 100 ? GetIntField(BODY_MUSCLE) : 99);
            }
            set
            {
                SetField(BODY_MUSCLE, value);
            }
        }

        public int BodyFat
        {
            get
            {
                return (GetIntField(BODY_FAT) < 100 ? GetIntField(BODY_FAT) : 99);
            }
            set
            {
                SetField(BODY_FAT, value);
            }
        }

        public int EquipmentShoes
        {
            get
            {
                return (GetIntField(EQP_SHOES) < 100 ? GetIntField(EQP_SHOES) : 99);
            }
            set
            {
                SetField(EQP_SHOES, value);
            }
        }

        public int EquipmentPadHeight
        {
            get
            {
                return (GetIntField(EQP_PAD_HEIGHT) < 100 ? GetIntField(EQP_PAD_HEIGHT) : 99);
            }
            set
            {
                SetField(EQP_PAD_HEIGHT, value);
            }
        }

        public int EquipmentPadWidth
        {
            get
            {
                return (GetIntField(EQP_PAD_WIDTH) < 100 ? GetIntField(EQP_PAD_WIDTH) : 99);
            }
            set
            {
                SetField(EQP_PAD_WIDTH, value);
            }
        }

        public int EquipmentPadShelf
        {
            get
            {
                return (GetIntField(EQP_PAD_SHELF) < 100 ? GetIntField(EQP_PAD_SHELF) : 99);
            }
            set
            {
                SetField(EQP_PAD_SHELF, value);
            }
        }

        public int EquipmentFlakJacket
        {
            get
            {
                return (GetIntField(EQP_FLAK_JACKET) < 100 ? GetIntField(EQP_FLAK_JACKET) : 99);
            }
            set
            {
                SetField(EQP_FLAK_JACKET, value);
            }
        }

        public int ArmsMuscle
        {
            get
            {
                return (GetIntField(ARMS_MUSCLE) < 100 ? GetIntField(ARMS_MUSCLE) : 99);
            }
            set
            {
                SetField(ARMS_MUSCLE, value);
            }
        }

        public int ArmsFat
        {
            get
            {
                return (GetIntField(ARMS_FAT) < 100 ? GetIntField(ARMS_FAT) : 99);
            }
            set
            {
                SetField(ARMS_FAT, value);
            }
        }

        public int LegsThighMuscle
        {
            get
            {
                return (GetIntField(LEGS_THIGH_MUSCLE) < 100 ? GetIntField(LEGS_THIGH_MUSCLE) : 99);
            }
            set
            {
                SetField(LEGS_THIGH_MUSCLE, value);
            }
        }

        public int LegsThighFat
        {
            get
            {
                return (GetIntField(LEGS_THIGH_FAT) < 100 ? GetIntField(LEGS_THIGH_FAT) : 99);
            }
            set
            {
                SetField(LEGS_THIGH_FAT, value);
            }
        }

        public int LegsCalfMuscle
        {
            get
            {
                return (GetIntField(LEGS_CALF_MUSCLE) < 100 ? GetIntField(LEGS_CALF_MUSCLE) : 99);
            }
            set
            {
                SetField(LEGS_CALF_MUSCLE, value);
            }
        }

        public int LegsCalfFat
        {
            get
            {
                return (GetIntField(LEGS_CALF_FAT) < 100 ? GetIntField(LEGS_CALF_FAT) : 99);
            }
            set
            {
                SetField(LEGS_CALF_FAT, value);
            }
        }

        public int RearRearFat
        {
            get
            {
                return (GetIntField(REAR_FAT) < 100 ? GetIntField(REAR_FAT) : 99);
            }
            set
            {
                SetField(REAR_FAT, value);
            }
        }

        public int RearShape
        {
            get
            {
                return (GetIntField(REAR_SHAPE) < 100 ? GetIntField(REAR_SHAPE) : 99);
            }
            set
            {
                SetField(REAR_SHAPE, value);
            }
        }

        public bool EyePaint
        {
            get { return GetIntField(EYE_PAINT) == 1; }
            set { SetField(EYE_PAINT, Convert.ToInt32(value)); }
            
        }

        public int CareerPhase
        {
            get
            {
                return GetIntField(CAREER_PHASE);
            }
            set
            {
                SetField(CAREER_PHASE, value);
            }
        }

        public int FaceShape
        {
            get
            {
                return (GetIntField(FACE_SHAPE) < 21 ? GetIntField(FACE_SHAPE) : 20);
            }
            set
            {
                SetField(FACE_SHAPE, value);
            }
        }

        #endregion
        public int PreviousTotalSalary
        {
            get { return GetIntField(PREVIOUS_TOTAL_SALARY); }
            set { SetField(PREVIOUS_TOTAL_SALARY, value); }
        }
        public int Pwin
        {
            get { return GetIntField(PWIN); }
            set { SetField(PWIN, value); }
        }

        // 2007
        public int Ego
        {
            get
            {
                return GetIntField(PLAYER_EGO);
            }
            set
            {
                SetField(PLAYER_EGO, value);
            }
        }
        public int PlayerValue
        {
            get
            {
                return GetIntField(PLAYER_VALUE);
            }
            set
            {
                SetField(PLAYER_VALUE, value);
            }
        }
        public int PlayerRole
        {
            get
            {
                return GetIntField(PLAYER_ROLE);
            }
            set
            {
                SetField(PLAYER_ROLE, value);
            }
        }        
        // 2008
        public int PlayerWeapon
        {
            get
            {
                return GetIntField(PLAYER_WEAPON);
            }
            set
            {
                SetField(PLAYER_WEAPON, value);
            }
        }
        public int YearsWithTeam
        {
            get { return GetIntField(YEARS_WITH_TEAM); }
            set { SetField(YEARS_WITH_TEAM, value); }
        }


        #endregion

        public int GetRating(int AttributeID)
        {
            switch (AttributeID)
            {
                case (int)Rating.ACC:
                    return Acceleration;                
                case (int)Rating.AGI:
                    return Agility;
                case (int)Rating.AWR:
                    return Awareness;
                case (int)Rating.BTK:
                    return BreakTackle;
                case (int)Rating.CAR:
                    return Carrying;
                case (int)Rating.CTH:
                    return Catching;
                case (int)Rating.INJ:
                    return Injury;
                case (int)Rating.JMP:
                    return Jumping;
                case (int)Rating.KAC:
                    return KickAccuracy;
                case (int)Rating.KPW:
                    return KickPower;
                case (int)Rating.KRT:
                    return KickReturn;                
                case (int)Rating.PBK:
                    return PassBlocking;
                case (int)Rating.RBK:
                    return RunBlocking;
                case (int)Rating.SPD:
                    return Speed;                
                case (int)Rating.STR:
                    return Strength;
                case (int)Rating.TAK:
                    return Tackle;                
                case (int)Rating.THA:
                    return ThrowAccuracy;
                case (int)Rating.THP:
                    return ThrowPower;
                case (int)Rating.IMP:
                    return Importance;
            }

            return -1;
        }




        #region Madden Draft Edit

        public int GetAttribute(int AttributeID)
        {
            switch (AttributeID)
            {
                case (int)MaddenAttribute.ACC:
                    return Acceleration;
                case (int)MaddenAttribute.AGE:
                    return Age;
                case (int)MaddenAttribute.AGI:
                    return Agility;
                case (int)MaddenAttribute.AWR:
                    return Awareness;
                case (int)MaddenAttribute.BTK:
                    return BreakTackle;
                case (int)MaddenAttribute.CAR:
                    return Carrying;
                case (int)MaddenAttribute.CTH:
                    return Catching;
                case (int)MaddenAttribute.INJ:
                    return Injury;
                case (int)MaddenAttribute.JMP:
                    return Jumping;
                case (int)MaddenAttribute.KAC:
                    return KickAccuracy;
                case (int)MaddenAttribute.KPR:
                    return KickPower;
                case (int)MaddenAttribute.KRT:
                    return KickReturn;
                case (int)MaddenAttribute.OVR:
                    return Overall;
                case (int)MaddenAttribute.PBK:
                    return PassBlocking;
                case (int)MaddenAttribute.RBK:
                    return RunBlocking;
                case (int)MaddenAttribute.SPD:
                    return Speed;
                case (int)MaddenAttribute.STA:
                    return Stamina;
                case (int)MaddenAttribute.STR:
                    return Strength;
                case (int)MaddenAttribute.TAK:
                    return Tackle;
                case (int)MaddenAttribute.TGH:
                    return Toughness;
                case (int)MaddenAttribute.THA:
                    return ThrowAccuracy;
                case (int)MaddenAttribute.THP:
                    return ThrowPower;
                case (int)MaddenAttribute.YRP:
                    return YearsPro;
                //2007
                case (int)MaddenAttribute.EGO:
                    return Ego;
                case (int)MaddenAttribute.VAL:
                    return PlayerValue;
            }

            return -1;
        }

        public void SetAttribute(int AttributeID, int value)
        {
            value = Math.Min(99, Math.Max(0, value));

            switch (AttributeID)
            {
                case (int)MaddenAttribute.ACC:
                    Acceleration = value;
                    break;
                case (int)MaddenAttribute.AGE:
                    Age = value;
                    break;
                case (int)MaddenAttribute.AGI:
                    Agility = value;
                    break;
                case (int)MaddenAttribute.AWR:
                    Awareness = value;
                    break;
                case (int)MaddenAttribute.BTK:
                    BreakTackle = value;
                    break;
                case (int)MaddenAttribute.CAR:
                    Carrying = value;
                    break;
                case (int)MaddenAttribute.CTH:
                    Catching = value;
                    break;
                case (int)MaddenAttribute.INJ:
                    Injury = value;
                    break;
                case (int)MaddenAttribute.JMP:
                    Jumping = value;
                    break;
                case (int)MaddenAttribute.KAC:
                    KickAccuracy = value;
                    break;
                case (int)MaddenAttribute.KPR:
                    KickPower = value;
                    break;
                case (int)MaddenAttribute.KRT:
                    KickReturn = value;
                    break;
                case (int)MaddenAttribute.OVR:
                    Overall = value;
                    break;
                case (int)MaddenAttribute.PBK:
                    PassBlocking = value;
                    break;
                case (int)MaddenAttribute.RBK:
                    RunBlocking = value;
                    break;
                case (int)MaddenAttribute.SPD:
                    Speed = value;
                    break;
                case (int)MaddenAttribute.STA:
                    Stamina = value;
                    break;
                case (int)MaddenAttribute.STR:
                    Strength = value;
                    break;
                case (int)MaddenAttribute.TAK:
                    Tackle = value;
                    break;
                case (int)MaddenAttribute.TGH:
                    Toughness = value;
                    break;
                case (int)MaddenAttribute.THA:
                    ThrowAccuracy = value;
                    break;
                case (int)MaddenAttribute.THP:
                    ThrowPower = value;
                    break;
                case (int)MaddenAttribute.YRP:
                    YearsPro = value;
                    break;
                //2007
                case (int)MaddenAttribute.EGO:
                    Ego = value;
                    break;
                case (int)MaddenAttribute.VAL:
                    PlayerValue = value;
                    break;

            }
        }

        public string RatingsLine(string[] attributes)
        {
            string toReturn = "";
            string last = attributes[attributes.Length - 1];

            foreach (string s in attributes)
            {
                toReturn += GetIntField(s);

                if (s != last)
                {
                    toReturn += "\t";
                }
            }

            return toReturn;
        }

        public void ImportWeeklyData(string[] attributes, string[] ratings)
        {
            int index = 0;
            foreach (string s in attributes)
            {
                if (s == "PGID") { index++; continue; }
                SetField(s, Int32.Parse(ratings[index]));
                index++;
            }
        }

        public void ImportData(List<string> playerData, int version)
        {
            int index = 0;
            foreach (string s in editorModel.DraftClassFields[version - 1])
            {
                if (ContainsStringField(s))
                {
                    SetField(s, playerData[index]);
                }
                else if (ContainsIntField(s))
                {
                    SetField(s, Int32.Parse(playerData[index]));
                }
                else
                {
                    Trace.WriteLine("Severe Error!  Player does not contain field " + s + "!  Returning...");
                    return;
                }

                index++;
            }
        }
                
        public int CalculateOverallRating(int positionId)
		{
			return CalculateOverallRating(positionId, false);
		}

		public int CalculateOverallRating(int positionId, bool withHeightAndWeight)
		{
			double tempOverall = 0;

			if (withHeightAndWeight)
			{
				LocalMath lmath = new LocalMath(MaddenFileVersion.Ver2006);
				tempOverall += lmath.HeightWeightAdjust(this, positionId);
			}

			switch (positionId)
			{
				case (int)MaddenPositions.QB:
					tempOverall += (((double)ThrowPower - 50) / 10) * 4.9;
					tempOverall += (((double)ThrowAccuracy - 50) / 10) * 5.8;
					tempOverall += (((double)BreakTackle - 50) / 10) * 0.8;
					tempOverall += (((double)Agility - 50) / 10) * 0.8;
					tempOverall += (((double)Awareness - 50) / 10) * 4.0;
					tempOverall += (((double)Speed - 50) / 10) * 2.0;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
					break;
				case (int)MaddenPositions.HB:
					tempOverall += (((double)PassBlocking - 50) / 10) * 0.33;
					tempOverall += (((double)BreakTackle - 50) / 10) * 3.3;
					tempOverall += (((double)Carrying - 50) / 10) * 2.0;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.8;
					tempOverall += (((double)Agility - 50) / 10) * 2.8;
					tempOverall += (((double)Awareness - 50) / 10) * 2.0;
					tempOverall += (((double)Strength - 50) / 10) * 0.6;
					tempOverall += (((double)Speed - 50) / 10) * 3.3;
					tempOverall += (((double)Catching - 50) / 10) * 1.4;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 27,1);
					break;
				case (int)MaddenPositions.FB:
					tempOverall+= (((double)PassBlocking - 50) / 10) * 1.0;
					tempOverall+= (((double)RunBlocking - 50) / 10) * 7.2;
					tempOverall+= (((double)BreakTackle - 50) / 10) * 1.8;
					tempOverall+= (((double)Carrying - 50) / 10) * 1.8;
					tempOverall+= (((double)Acceleration - 50) / 10) * 1.8;
					tempOverall+= (((double)Agility - 50) / 10) * 1.0;
					tempOverall+= (((double)Awareness - 50) / 10) * 2.8;
					tempOverall+= (((double)Strength - 50) / 10) * 1.8;
					tempOverall+= (((double)Speed - 50) / 10) * 1.8;
					tempOverall+= (((double)Catching - 50) / 10) * 5.2;
					tempOverall= (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 39,1);
					break;
				case (int)MaddenPositions.WR:
					tempOverall += (((double)BreakTackle - 50) / 10) * 0.8;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.3;
					tempOverall += (((double)Agility - 50) / 10) * 2.3;
					tempOverall += (((double)Awareness - 50) / 10) * 2.3;
					tempOverall += (((double)Strength - 50) / 10) * 0.8;
					tempOverall += (((double)Speed - 50) / 10) * 2.3;
					tempOverall += (((double)Catching - 50) / 10) * 4.75;
					tempOverall += (((double)Jumping - 50) / 10) * 1.4;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 26, 1);
					break;
				case (int)MaddenPositions.TE:
					tempOverall += (((double)Speed - 50) / 10) * 2.65;
					tempOverall += (((double)Strength - 50) / 10) * 2.65;
					tempOverall += (((double)Awareness - 50) / 10) * 2.65;
					tempOverall += (((double)Agility - 50) / 10) * 1.25;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.25;
					tempOverall += (((double)Catching - 50) / 10) * 5.4;
					tempOverall += (((double)BreakTackle - 50) / 10) * 1.2;
					tempOverall += (((double)PassBlocking - 50) / 10) * 1.2;
					tempOverall += (((double)RunBlocking - 50) / 10) * 5.4;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 35, 1);
					break;
				case (int)MaddenPositions.LT:
				case (int)MaddenPositions.RT:
					tempOverall += (((double)Speed - 50) / 10) * 0.8;
					tempOverall += (((double)Strength - 50) / 10) * 3.3;
					tempOverall += (((double)Awareness - 50) / 10) * 3.3;
					tempOverall += (((double)Agility - 50) / 10) * 0.8;
					tempOverall += (((double)Acceleration - 50) / 10) * 0.8;
					tempOverall += (((double)PassBlocking - 50) / 10) * 4.75;
					tempOverall += (((double)RunBlocking - 50) / 10) * 3.75;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 26, 1);
					break;
				case (int)MaddenPositions.LG:
				case (int)MaddenPositions.RG:
				case (int)MaddenPositions.C:
					tempOverall += (((double)Speed - 50) / 10) * 1.7;
					tempOverall += (((double)Strength - 50) / 10) * 3.25;
					tempOverall += (((double)Awareness - 50) / 10) * 3.25;
					tempOverall += (((double)Agility - 50) / 10) * 0.8;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.7;
					tempOverall += (((double)PassBlocking - 50) / 10) * 3.25;
					tempOverall += (((double)RunBlocking - 50) / 10) * 4.8;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
					break;
				case (int)MaddenPositions.LE:
				case (int)MaddenPositions.RE:
					tempOverall += (((double)Speed - 50) / 10) * 3.75;
					tempOverall += (((double)Strength - 50) / 10) * 3.75;
					tempOverall += (((double)Awareness - 50) / 10) * 1.75;
					tempOverall += (((double)Agility - 50) / 10) * 1.75;
					tempOverall += (((double)Acceleration - 50) / 10) * 3.8;
					tempOverall += (((double)Tackle - 50) / 10) * 5.5;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
					break;
				case (int)MaddenPositions.DT:
					tempOverall += (((double)Speed - 50) / 10) * 1.8;
					tempOverall += (((double)Strength - 50) / 10) * 5.5;
					tempOverall += (((double)Awareness - 50) / 10) * 3.8;
					tempOverall += (((double)Agility - 50) / 10) * 1;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.8;
					tempOverall += (((double)Tackle - 50) / 10) * 4.55;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 29, 1);
					break;
				case (int)MaddenPositions.LOLB:
				case (int)MaddenPositions.ROLB:
					tempOverall += (((double)Speed - 50) / 10) * 3.75;
					tempOverall += (((double)Strength - 50) / 10) * 2.4;
					tempOverall += (((double)Awareness - 50) / 10) * 3.6;
					tempOverall += (((double)Agility - 50) / 10) * 2.4;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.3;
					tempOverall += (((double)Catching - 50) / 10) * 1.3;
					tempOverall += (((double)Tackle - 50) / 10) * 4.8;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 29, 1);
					break;
				case (int)MaddenPositions.MLB:
					tempOverall += (((double)Speed - 50) / 10) * 0.75;
					tempOverall += (((double)Strength - 50) / 10) * 3.4;
					tempOverall += (((double)Awareness - 50) / 10) * 5.2;
					tempOverall += (((double)Agility - 50) / 10) * 1.65;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.75;
					tempOverall += (((double)Tackle - 50) / 10) * 5.2;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 27, 1);
					break;
				case (int)MaddenPositions.CB:
					tempOverall += (((double)Speed - 50) / 10) * 3.85;
					tempOverall += (((double)Strength - 50) / 10) * 0.9;
					tempOverall += (((double)Awareness - 50) / 10) * 3.85;
					tempOverall += (((double)Agility - 50) / 10) * 1.55;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.35;
					tempOverall += (((double)Catching - 50) / 10) * 3;
					tempOverall += (((double)Jumping - 50) / 10) * 1.55;
					tempOverall += (((double)Tackle - 50) / 10) * 1.55;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 28, 1);
					break;
				case (int)MaddenPositions.FS:
					tempOverall += (((double)Speed - 50) / 10) * 3.0;
					tempOverall += (((double)Strength - 50) / 10) * 0.9;
					tempOverall += (((double)Awareness - 50) / 10) * 4.85;
					tempOverall += (((double)Agility - 50) / 10) * 1.5;
					tempOverall += (((double)Acceleration - 50) / 10) * 2.5;
					tempOverall += (((double)Catching - 50) / 10) * 3.0;
					tempOverall += (((double)Jumping - 50) / 10) * 1.5;
					tempOverall += (((double)Tackle - 50) / 10) * 2.5;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
					break;
				case (int)MaddenPositions.SS:
					tempOverall += (((double)Speed - 50) / 10) * 3.2;
					tempOverall += (((double)Strength - 50) / 10) * 1.7;
					tempOverall += (((double)Awareness - 50) / 10) * 4.75;
					tempOverall += (((double)Agility - 50) / 10) * 1.7;
					tempOverall += (((double)Acceleration - 50) / 10) * 1.7;
					tempOverall += (((double)Catching - 50) / 10) * 3.2;
					tempOverall += (((double)Jumping - 50) / 10) * 0.9;
					tempOverall += (((double)Tackle - 50) / 10) * 3.2;
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall) + 30, 1);
					break;
				case (int)MaddenPositions.P:
					tempOverall = (double)(-183 + 0.218*Awareness + 1.5 * KickPower + 1.33 * KickAccuracy);
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall));
					break;
				case (int)MaddenPositions.K:
					tempOverall = (double)(-177 + 0.218*Awareness + 1.28 * KickPower + 1.47 * KickAccuracy);
					tempOverall = (int)Math.Round((decimal)Convert.ToInt32(tempOverall));
					break;
			}

			if (tempOverall < 0)
			{
				tempOverall = 0;
			}
			if (tempOverall > 99)
			{
				tempOverall = 99;
			}

			return (int)tempOverall;
		}
        
        public DataGridViewRow GetDataRow(int positionId)
        {
            DataGridViewRow viewRow = new DataGridViewRow();

            DataGridViewTextBoxCell posCell = new DataGridViewTextBoxCell();
            posCell.Value = Enum.GetNames(typeof(MaddenPositions))[PositionId];
            DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
            nameCell.Value = FirstName + " " + LastName;
            DataGridViewTextBoxCell ovrCell = new DataGridViewTextBoxCell();
            ovrCell.Value = CalculateOverallRating(positionId);
            DataGridViewTextBoxCell playerCell = new DataGridViewTextBoxCell();
            playerCell.Value = this;
            viewRow.Cells.Add(posCell);
            viewRow.Cells.Add(nameCell);
            viewRow.Cells.Add(ovrCell);
            viewRow.Cells.Add(playerCell);

            posCell.ReadOnly = true;
            nameCell.ReadOnly = true;
            ovrCell.ReadOnly = true;
            playerCell.ReadOnly = true;

            return viewRow;
        }

        #endregion
        
        #region Salary Signing Bonus Functions

        
        public int GetSalaryAtYear(int year)
		{
			string key = "PSA" + year;

			if (ContainsField(key))
			{
				return GetIntField(key);
			}
			else
			{
				return (int)estYearlySalary[year];
			}
		}

		public int GetSigningBonusAtYear(int year)
		{
			string key = "PSB" + year;

			if (ContainsField(key))
			{
				return GetIntField(key);
			}
			else
			{
				return (int)estSigningBonusArray[year];
			}
		}

		// fix all the salary cap functions

        public int CapHit
		{
			get
			{
				return capHit;
			}
		}	

        public void FixCurrentSalary()
        {            
            if (ContainsField(SALARY_YEAR_0))
            {
                if (ContractYearsLeft == 0)
                {
                    CurrentSalary = 0;
                    return;
                }
                int tempsal = CurrentSalary;
                int year = ContractLength - ContractYearsLeft;
                CurrentSalary = GetIntField("PSA" + year) + GetIntField("PSB" + year);                
            }
        }
        		
        private void CalculateCapHit(bool causeDirty)
        {
            if (ContractLength == 0 || ContractLength > 7)
                return;
            double perc = 1;

            // Salaries are not allowed to increase more than 30% each year, for rookies this is 25%
            double x = 1.30;
            if (YearsPro == 0)
                x = 1.25;
            for (int t = 1; t < ContractLength; t++)
            {
                perc += Math.Pow(x, t);
            }
            double tempsal = (double)(TotalSalary - BonusTotal)/ (perc * 100);

            //reset values
            for (int i = 0; i < 7; i++)
            {
                estYearlySalary[i] = 0;
                estSigningBonusArray[i] = 0;
            }
            double last = 0;
            for (int i = 0; i < ContractLength; i++)
            {
                if (i < ContractLength - 1)
                    estYearlySalary[i] = Math.Round(tempsal * Math.Pow(x, i) * 100, 0);
                else estYearlySalary[i] = TotalSalary - BonusTotal - last;
                estSigningBonusArray[i] = (double)BonusTotal / ContractLength;
                last += estYearlySalary[i];
            }

            if (ContainsField(SALARY_YEAR_0))
            {
                //We are a franchise file so save back our yearly stuff
                for (int i = 0; i < 7; i++)
                {
                    string key = "PSA" + i;
                    SetField(key, (int)estYearlySalary[i], causeDirty);
                    key = "PSB" + i;
                    SetField(key, (int)estSigningBonusArray[i], causeDirty);
                }
            }

            if (causeDirty)
                FixCurrentSalary();
        }

		#endregion

		
        


	}
}