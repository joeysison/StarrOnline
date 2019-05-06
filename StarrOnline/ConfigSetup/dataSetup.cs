using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace StarrOnline.ConfigSetup
{
    public class dataSetup
    {

        string dirPath = "c:\\StarrOnline\\";

        public void createXMLDBFolder()
        {
            try
            {
                string xmlFilePath = dirPath + "StarrConfig.xml";
                //string xmlFilePathPNR = dirPath + "TARSetup\\";

                if (xmlFilePath.Equals("")) return;

                DirectoryInfo xmlDirInfo = null;
                FileInfo setupTable = new FileInfo(xmlFilePath);
                xmlDirInfo = new DirectoryInfo(setupTable.DirectoryName);
                if (!xmlDirInfo.Exists) xmlDirInfo.Create();

                //DirectoryInfo xmlDirInfoPNR = null;
                //FileInfo setupTablePNR = new FileInfo(xmlFilePathPNR);
                //xmlDirInfoPNR = new DirectoryInfo(setupTablePNR.DirectoryName);
                //if (!xmlDirInfoPNR.Exists) xmlDirInfoPNR.Create();

                XmlTextWriter xtw;
                if (!setupTable.Exists)
                {
                    xtw = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
                    xtw.WriteStartDocument();
                    xtw.WriteStartElement("Settings");
                    xtw.WriteEndElement();
                    xtw.Close();
                    initSetupTable();
                }
                //createLogsTable();
            }
            catch (Exception x)
            {
                //MessageBox.Show(x.GetBaseException().ToString(), "Error");
                throw new LibraryErrorException("createXMLDBFolder" + x.Message);
            }
        }

        public void initSetupTable()
        {
            try
            {
                string xmlFilePath = dirPath + "StarrConfig.xml";
                XmlDocument xd = new XmlDocument();
                FileStream lfile = new FileStream(xmlFilePath, FileMode.Open);
                xd.Load(lfile);

                //nodes
                XmlElement h1 = xd.CreateElement("ConfigurationSetup");
                XmlElement a1 = xd.CreateElement("SelectedGDS");
                XmlElement a2 = xd.CreateElement("AgentName");
                XmlElement a3 = xd.CreateElement("AgentIDAmadeus");
                XmlElement a4 = xd.CreateElement("AgentIDSabre");
                XmlElement a5 = xd.CreateElement("DatabaseConnection");
                XmlElement a6 = xd.CreateElement("AppName");
                XmlElement a7 = xd.CreateElement("applyFilter");
                XmlElement a8 = xd.CreateElement("exportFile");
                XmlElement a9 = xd.CreateElement("reportFiles");
                XmlElement a10 = xd.CreateElement("reportSOA");
                XmlElement a11 = xd.CreateElement("reportGRP");

                //Value
                XmlText b1 = xd.CreateTextNode("Amadeus");
                XmlText b2 = xd.CreateTextNode("Joey");
                XmlText b3 = xd.CreateTextNode("0427");
                XmlText b4 = xd.CreateTextNode("0427");
                XmlText b5 = xd.CreateTextNode("Data Source = starrdb.casrzvv9fyy4.ap-southeast-1.rds.amazonaws.com,1433; Initial Catalog = starrDB; User ID = aUserDB; password=password123");
                XmlText b6 = xd.CreateTextNode("-- STAR ONLINE --");
                XmlText b7 = xd.CreateTextNode("SUPERVISOR");
                XmlText b8 = xd.CreateTextNode("d:");
                XmlText b9 = xd.CreateTextNode("\\\\192.168.0.5\\PhilscanSharedFolder\\IT\\IT-USB Shared files\\BSI Data\\StarrOnline\\Report");
                XmlText b10 = xd.CreateTextNode("vw_SOA_Philscan");
                XmlText b11 = xd.CreateTextNode("vw_USERGRP_Philscan");

                //add
                a1.AppendChild(b1);
                a2.AppendChild(b2);
                a3.AppendChild(b3);
                a4.AppendChild(b4);
                a5.AppendChild(b5);
                a6.AppendChild(b6);
                a7.AppendChild(b7);
                a8.AppendChild(b8);
                a9.AppendChild(b9);
                a10.AppendChild(b10);
                a11.AppendChild(b11);

                //write
                h1.AppendChild(a1);
                h1.AppendChild(a2);
                h1.AppendChild(a3);
                h1.AppendChild(a4);
                h1.AppendChild(a5);
                h1.AppendChild(a6);
                h1.AppendChild(a7);
                h1.AppendChild(a8);
                h1.AppendChild(a9);
                h1.AppendChild(a10);
                h1.AppendChild(a11);

                xd.DocumentElement.AppendChild(h1);
                
                lfile.Close();
                xd.Save(xmlFilePath);
            }
            catch (Exception x)
            {
                throw new LibraryErrorException("initSetupTable" + x.Message);
            }
        }

        public string readClientList(string field)
        {
            try
            {
                string xmlFilePath = dirPath + "StarrConfig.xml";
                XmlDocument xdoc = new XmlDocument();
                FileStream up = new FileStream(xmlFilePath, FileMode.Open);
                xdoc.Load(up);

                XmlNodeList elemList = xdoc.GetElementsByTagName("Client");

                for (int i = 0; i < elemList.Count; i++)
                {
                    if (elemList[i].Attributes["id"].Value.Equals(field))
                    {
                        return elemList[i].Attributes["name"].Value;
                    }
                }

                return "";
            }
            catch (Exception x)
            {
                throw new LibraryErrorException("readClientList" + x.Message);
            }
        }

        public string readSetupTable(string field)
        {
            string tmp = "";
            try
            {
                string xmlFilePath = dirPath + "StarrConfig.xml";
                XmlDocument xdoc = new XmlDocument();
                FileStream up = new FileStream(xmlFilePath, FileMode.Open);
                xdoc.Load(up);

                XmlElement sel = (XmlElement)xdoc.GetElementsByTagName(field)[0];

                tmp = sel.InnerText;

                up.Close();
            }
            catch (Exception x)
            {
                throw new LibraryErrorException("readSetupTable" + x.Message);
                //MessageBox.Show(x.GetBaseException().ToString(), "Error");

            }
            return tmp;
        }

        public void updateSetupTableSelected(string field, string fieldValue, string setupTable)
        {
            try
            {
                string xmlFilePath = dirPath + "StarrConfig.xml";

                XElement xelement = XElement.Load(xmlFilePath);

                var ms = from stp in xelement.Elements(setupTable)
                         select stp;
                foreach (XElement xEle in ms)
                {
                    xEle.SetElementValue(field, fieldValue);
                }

                xelement.Save(xmlFilePath);
            }
            catch (Exception x)
            {
                throw new LibraryErrorException("updateSetupTableSelected" + x.Message);
            }
        }

    }
}
