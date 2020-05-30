using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Coins_Database.Actions
{
    class XML
    {
        static string ConfigFile = "configs.xml";

        public static string CheckOrCreateXML()
        {
            XDocument XDoc = new XDocument();
            XElement EConnection = new XElement("connection");
            if (!File.Exists(ConfigFile))
            {
                XAttribute AServer = new XAttribute("server", "127.0.0.1");
                XAttribute APort = new XAttribute("port", "5432");
                CreateFile(XDoc, EConnection, AServer, APort);
            }
            return ConfigFile;
        }

        public static void RewriteXML(string Port, string Ip)
        {
            XDocument XDoc = new XDocument();
            XElement EConnection = new XElement("connection");
            XAttribute AServer = new XAttribute("server", Ip);
            XAttribute APort = new XAttribute("port", Port);
            CreateFile(XDoc, EConnection, AServer, APort);
        }

        static void CreateFile(XDocument XDoc, XElement EConnection, XAttribute AServer, XAttribute APort)
        {
            EConnection.Add(AServer);
            EConnection.Add(APort);
            XDoc.Add(EConnection);
            XDoc.Save(ConfigFile);
        }

        public static List<string> ReadXML(string Filename)
        {
            XDocument XDocument = XDocument.Load(Filename);
            List<string> Parameters = new List<string>
            {
                XDocument.Element("connection").Attribute("server").Value.ToString(),
                XDocument.Element("connection").Attribute("port").Value.ToString()
            };
            return Parameters;
        }
    }
}
