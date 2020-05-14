using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Coins_Database.Actions
{
    class XML
    {
        public static string CheckOrCreateXML()
        {
            var config_file = "configs.xml";
            if (!File.Exists(config_file))
            {
                XDocument xDoc = new XDocument();
                XElement EConnection = new XElement("connection");
                XAttribute AServer = new XAttribute("server", "127.0.0.1");
                XAttribute APort = new XAttribute("port", "5432");
                EConnection.Add(AServer);
                EConnection.Add(APort);
                xDoc.Add(EConnection);
                xDoc.Save(config_file);
            }
            return config_file;
        }

        public static List<string> ReadXML(string filename)
        {
            XDocument xDocument = XDocument.Load(filename);
            List<string> Parameters = new List<string>
            {
                xDocument.Element("connection").Attribute("server").Value.ToString(),
                xDocument.Element("connection").Attribute("port").Value.ToString()
            };
            return Parameters;
        }
    }
}
