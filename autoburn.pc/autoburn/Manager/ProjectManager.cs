﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Autoburn.Manager
{
    class ProjectManager
    {

        private string _configDir = ProgramInfo.CONFIGDIRPATH;
        public ProjectManager()
        {
           // Init();
        }

        private void Init()
        {
            if (!Directory.Exists(_configDir))
            {
                Directory.CreateDirectory(_configDir);
            }
            var f = new FileInfo(ProgramInfo.CONFIGFILE);
            if (!f.Exists)
            {
                // init a xml file.
                XmlTextWriter InitXmlTextWriter = new XmlTextWriter(ProgramInfo.CONFIGFILE, null);
                InitXmlTextWriter.Formatting = Formatting.Indented;
                InitXmlTextWriter.WriteStartDocument(true);
                InitXmlTextWriter.WriteStartElement(ConfigInfo.TYPE_E_CONFIG);
                InitXmlTextWriter.WriteAttributeString(ConfigInfo.TYPE_A_CREATETIME, DateTime.Now.ToString());
                InitXmlTextWriter.WriteAttributeString(ConfigInfo.TYPE_A_VERSION, Assembly.GetExecutingAssembly().GetName().Version.ToString());

                InitXmlTextWriter.WriteEndElement();
                InitXmlTextWriter.Flush();
                InitXmlTextWriter.Close();
                InitXmlTextWriter.Dispose();
            }

            _xmlDocument.Load(ProgramInfo.CONFIGFILE);
        }
        XmlDocument _xmlDocument = new XmlDocument();

        public List<string> GetSavedChooseChipHistory()
        {
            List<string> historyname = new List<string>();
            var chiphistorynode = _xmlDocument.SelectSingleNode(_chiphistoryel);
            var historyitem = chiphistorynode?.SelectNodes(_chiphistoryel + "/" + ConfigInfo.TYPE_E_CHIPCHOOSEHISTORYITEM);
            if (historyitem != null)
            {
                foreach (XmlNode itemnode in historyitem)
                {
                    var itemel = itemnode as XmlElement;
                    historyname.Add(itemel.GetAttribute(ConfigInfo.TYPE_A_CHIPCHOOSEHISTORYNAME));
                }
            }
            historyname.Reverse();
            return historyname;
        }

        private string _chiphistoryel = "/" + ConfigInfo.TYPE_E_CONFIG + "/" + ConfigInfo.TYPE_E_CHIPCHOOSEHISTORY;
        public void PutChooseChipHistoryItem(string history)
        {
            if (String.IsNullOrEmpty(history))
            {
                return;
            }
            var chiphistorynode = _xmlDocument.SelectSingleNode(_chiphistoryel);
            if (chiphistorynode == null)
            { //not exist create a node .
                var rootnode = _xmlDocument.SelectSingleNode(ConfigInfo.TYPE_E_CONFIG);
                var historyroot = _xmlDocument.CreateElement(ConfigInfo.TYPE_E_CHIPCHOOSEHISTORY);
                rootnode?.AppendChild(historyroot);
            }
            chiphistorynode = _xmlDocument.SelectSingleNode(_chiphistoryel);

            var historyitemlist = chiphistorynode?.SelectNodes(_chiphistoryel + "/" + ConfigInfo.TYPE_E_CHIPCHOOSEHISTORYITEM);
            for (int i = 0; i < historyitemlist.Count; i++)
            {
                if (i < historyitemlist.Count - 10)
                { //去多.
                    chiphistorynode.RemoveChild(historyitemlist[i]);
                    //_xmlDocument.RemoveChild(historyitemlist[i]);
                }
                else
                {//去重
                    var itemel = historyitemlist[i] as XmlElement;
                    if (history.Equals(itemel.GetAttribute(ConfigInfo.TYPE_A_CHIPCHOOSEHISTORYNAME)))
                    {
                        chiphistorynode.RemoveChild(itemel);
                    }
                }
            }
            //  historyitemlist = chiphistorynode?.SelectNodes(_chiphistoryel + "/" + ConfigInfo.TYPE_E_CHIPCHOOSEHISTORYITEM);
            //     forea

            var historyitem = _xmlDocument.CreateElement(ConfigInfo.TYPE_E_CHIPCHOOSEHISTORYITEM);
            var historyitemname = _xmlDocument.CreateAttribute(ConfigInfo.TYPE_A_CHIPCHOOSEHISTORYNAME);
            historyitemname.Value = history;
            historyitem.Attributes.Append(historyitemname);
            chiphistorynode?.AppendChild(historyitem);
            _xmlDocument.Save(ProgramInfo.CONFIGFILE);
        }
    }
}