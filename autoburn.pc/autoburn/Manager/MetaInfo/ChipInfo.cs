﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoburn.Manager
{
    public class ChipInfo
    {
        //      <chipinfo vendor = "jsc" series="JSFXX">
        //<chip name = "JSFA9A2N73ABB-450" type="emmc" package="bma107" burner="ZY-DB107A0-A01" note="">  </chip>
        public const string TYPE_TABLE_NAME_CHIPINFO = "chipinfo";
        public const string TYPE_COLUMN_VENDOR = "vendor";
        public const string TYPE_COLUMN_SERIES = "series";
        public const string TYPE_COLUMN_NAME = "name";
        public const string TYPE_COLUMN_TYPE = "type";
        public const string TYPE_COLUMN_PACKAGE = "package";
        public const string TYPE_COLUMN_BURNER = "burner";
        public const string TYPE_COLUMN_NOTE = "note";

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" vendor: " + vendor);
            sb.Append(" series: " + series);
            sb.Append(" name: " + name);
            sb.Append(" type: " + type);
            sb.Append(" package: " + package);
            sb.Append(" burner: " + burner);
            sb.Append(" note: " + note);

            return sb.ToString();
        }

        private string _vendor;
        public string vendor
        {
            get
            {
                return _vendor;
            }
            set
            {
                _vendor = value;
            }
        }

        private string _series;
        public string series
        {
            get
            {
                return _series;
            }
            set
            {
                _series = value;
            }
        }
       
        private string _burner;
        public string burner
        {
            get
            {
                return _burner;
            }
            set
            {
                _burner = value;
            }
        }
        private string _note;
        public string note
        {
            get
            {
                return _note;
            }
            set
            {
                _note = value;
            }
        }

        private string _name;
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private string _type;
        public string type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }

        private string _package;
        public string package
        {
            get
            {
                return _package;
            }
            set
            {
                _package = value;
            }
        }
    }
}
