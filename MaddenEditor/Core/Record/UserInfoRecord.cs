﻿/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2018 StingRay68
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaddenEditor.Core;

namespace MaddenEditor.Core.Record
{
    public class UserInfoRecord : TableRecordModel
    {
        // UINF   2019
        public const string OFFENSIVE_PLAYBOOK = "PSpn";    //2019
        public const string FAVORITE_TEAM = "UFTI";
        public const string DEFENSIVE_PLAYBOOK = "UIDP";    //2019

        public UserInfoRecord(int record, TableModel tableModel, EditorModel EditorModel)
			: base(record, tableModel, EditorModel)
		{

		}

        public int PlaybookOFF
        {
            get { return GetIntField(OFFENSIVE_PLAYBOOK); }
            set { SetField(OFFENSIVE_PLAYBOOK, value); }
        }
        public int FavoriteTeam
        {
            get { return GetIntField(FAVORITE_TEAM); }
            set { SetField(FAVORITE_TEAM, value); }
        }
        public int PlaybookDEF
        {
            get { return GetIntField(DEFENSIVE_PLAYBOOK); }
            set { SetField(DEFENSIVE_PLAYBOOK, value); }
        }


    }
}
