﻿/******************************************************************************
 * MaddenAmp
 * Copyright (C) 2018 Stingray68
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
 *****************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MaddenEditor.Core;

namespace MaddenEditor.Db
{
    public class BEDB
    {
        // 24 bytes for header
        private UInt16 _db_id = 16964;
        private UInt16 _db_pad = 2048;              // 00 08
        private UInt32 _db_bigend = 0;              // Big endian or little endian
        private UInt32 _db_filelength;
        private UInt32 _db_headpad2;                // 00 00 00 00
        private UInt32 _db_table_count;
        private UInt32 _db_header_crc;              //  Checksum based on previous 20 bytes
       
        public UInt16 id
        {
            get { return _db_id; }
            set { _db_id = value; }
        }
        public UInt16 DBPad
        {
            get { return _db_pad; }
            set { _db_pad = value; }
        }
        public UInt32 Length
        {
            get { return _db_filelength; }
            set { _db_filelength = value; }
        }
        public UInt32 pad2
        {
            get { return _db_headpad2; }
            set { _db_headpad2 = value; }
        }
        public UInt32 TableCount
        {
            get { return _db_table_count; }
            set { _db_table_count = value; }
        }
        public UInt32 HeaderCRC
        {
            get { return _db_header_crc; }
            set { _db_header_crc = value; }
        }
        public bool isdb = true;

        public BEDB()
        {
            id = 16964;
            DBPad = 8;
            Length = 0;
            pad2 = 0;
            TableCount = 0;
            HeaderCRC = 0;
        }
        public bool Read(string filename)
        {
            BinaryReader binreader = new BinaryReader(File.Open(filename, FileMode.Open));
            if (binreader.ReadUInt16() != id)
            {
                isdb = false;
                binreader.Close();
                return false;
            }
            DBPad = binreader.ReadUInt16();
            _db_bigend = binreader.ReadUInt32();
            binreader.Close();

            if (_db_bigend == 1)
                return true;
            else return false;            
        }
        
    }
        
    public enum FBType
    {
        NA,
        Unknown,
        Roster,
        Franchise,
        Draft
    }
    
    public enum FBVersion
    {
        NA,
        Madden19,
        Madden20
    }
    
    public class FB
    {
        #region Header
        // 18 bytes
        public UInt32 FBCH = 1212367430; // FBCH
        public UInt32 UNKS = 1397444181; // UNKS
        public UInt16 FB_Version = 1;
        public UInt32 FB_InfoLength;
        public UInt32 DataFileLength;
        #endregion
        //
        #region Info
        // 44 bytes
        public UInt32 TotalLength;
        public UInt16 InfoYear;
        public UInt16 InfoMonth;
        public UInt16 InfoDay;
        public UInt16 InfoHour;
        public UInt16 InfoMin;
        public UInt16 InfoSec;
        //
        public UInt32 SerialVersion;  // 'RL2-' or 'RL3-'
        public byte[] Serial = new byte[28];       
        public UInt32 DataEntries;
        //
        public byte[] DB;        
        #endregion 
              
        public string database = "";
        string filename = "";
        public bool BigEndian = false;
        public FBType FileType = FBType.NA;
        public FBVersion FileVersion = FBVersion.NA;
        public BinaryReader binreader;
        public BinaryWriter binwriter;
        public UInt32 DraftClass = 2;
        public UInt32 DraftClassEntries = 450;
        
        public FB()
        {
            database = "";
            filename = "";
        }
        public FB(FB roster, FBType fbtype, MaddenFileVersion version)
        {
            if (fbtype == FBType.Draft)
            {
                this.filename = "DraftClass";
                this.FB_InfoLength = 52;
                if (version == MaddenFileVersion.Ver2019)
                {
                    this.DataFileLength = 149240;
                    this.TotalLength = 149292;
                }
                else if (version == MaddenFileVersion.Ver2020)
                {
                    this.DataFileLength = 152880;
                    this.TotalLength = 0x025564;
                }
                this.Serial = roster.Serial;
                this.DataEntries = DraftClassEntries;
            }
        }

        public void ChangeDraftClassVersion(MaddenFileVersion version)
        {
            if (version == MaddenFileVersion.Ver2019)
            {
                this.DataFileLength = 149240;
                this.TotalLength = 149292;
            }
            else if (version == MaddenFileVersion.Ver2020)
            {
                this.DataFileLength = 152880;
                this.TotalLength = 152932;
            }
        }

        public bool Extract(string filename)
        {
            this.filename = filename;
            binreader = new BinaryReader(File.Open(filename, FileMode.Open));
            long size = binreader.BaseStream.Length;

            UInt32 frostbyte = binreader.ReadUInt32();            
            if (frostbyte != FBCH)
            {
                // Not supported                
                binreader.Close();
                return false;
            }
            UNKS = binreader.ReadUInt32();
            FB_Version = binreader.ReadUInt16();
            FB_InfoLength = binreader.ReadUInt32();
            DataFileLength = binreader.ReadUInt32();
            TotalLength = binreader.ReadUInt32();
            InfoYear = binreader.ReadUInt16();
            InfoMonth = binreader.ReadUInt16();
            InfoDay = binreader.ReadUInt16();
            InfoHour = binreader.ReadUInt16();
            InfoMin = binreader.ReadUInt16();
            InfoSec = binreader.ReadUInt16();
            // Now at offset 34

            // Version specific Serial number
            int serlength = 28;
            UInt32 ver = binreader.ReadUInt32();
            binreader.BaseStream.Position -= 4;
           
            if (ver == 0x5F30324D)
                FileVersion = FBVersion.Madden20;
            else FileVersion = FBVersion.Madden19;

            Serial = binreader.ReadBytes(serlength);

            binreader.BaseStream.Position = 62;
            if (binreader.ReadUInt32() == 0x08004244)
                FileType = FBType.Roster;
            else
            {
                binreader.Close();
                return false;
            }

            binreader.BaseStream.Position = 62;
            DB = binreader.ReadBytes((int)size - 62);

            binreader.Close();            
            database = filename + ".db";

            binwriter = new BinaryWriter(File.Open(database, FileMode.Create));
            binwriter.Write(DB);
            binwriter.Close();

            return true;
        }

        public void RemoveDB()
        {
           if (File.Exists(database))
               File.Delete(database); 
        }
        
        public void Save()
        {
            // s68
            // return if File doesn't exist, Amp is calling shutdown function twice for some reason,
            // don't feel like tracking it down.
            if (!File.Exists(database))
                return;
            
            BinaryReader binreader = new BinaryReader(File.Open(database, FileMode.Open));
            byte[] db = binreader.ReadBytes((int)binreader.BaseStream.Length);
            binreader.Close();

            DataFileLength = (uint)db.Length;
            TotalLength = DataFileLength + 44;
            
            BinaryWriter binwriter = new BinaryWriter(File.Open(filename, FileMode.Create));
            binwriter.Write(FBCH);
            binwriter.Write(UNKS);
            binwriter.Write(FB_Version);
            binwriter.Write(FB_InfoLength);
            binwriter.Write(DataFileLength);
            binwriter.Write(TotalLength);
            
            #region Date/Time
            binwriter.Write((UInt16)InfoYear);
            binwriter.Write((UInt16)InfoMonth);
            binwriter.Write((UInt16)InfoDay);
            binwriter.Write((UInt16)InfoHour);
            binwriter.Write((UInt16)InfoMin);
            binwriter.Write((UInt16)InfoSec);
            #endregion

            binwriter.Write(Serial);
            binwriter.Write(db);
            binwriter.Close();                      
        }        
    }
}
