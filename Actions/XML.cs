using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Coins_Database.Actions
{
    class XML
    {
        public static string CheckOrCreateXML()
        {
            var ConfigFile = "configs.xml";
            if (!File.Exists(ConfigFile))
            {
                XDocument XDoc = new XDocument();
                XElement EConnection = new XElement("connection");
                XAttribute AServer = new XAttribute("server", "127.0.0.1");
                XAttribute APort = new XAttribute("port", "5432");
                EConnection.Add(AServer);
                EConnection.Add(APort);
                XDoc.Add(EConnection);
                XDoc.Save(ConfigFile);
            }
            return ConfigFile;
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
