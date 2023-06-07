using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FalloutMinigame.Objects
{
    /// <summary>
    ///     Třída sloužící pro správu savů.
    /// </summary>
    internal class SaveSystem
    {
        
        public static int SavePlayer(Player p)
        {
            string saveDirectory = "./Resource/";
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement rootElement = xmlDoc.CreateElement("player-data");
            rootElement.SetAttribute("name", p.Name);
            xmlDoc.AppendChild(rootElement);
            foreach(var l in p.playerStats)
            {
                XmlElement el = xmlDoc.CreateElement(l.Key);
                el.InnerText = l.Value.ToString();
                rootElement.AppendChild(el);
            }
            xmlDoc.Save(saveDirectory + p.Name + ".xml");

            return 1;
        }

        public static Player LoadPlayer(string path)
        {
            if(!File.Exists(path)) throw new FileNotFoundException();
            XmlDocument xml = new XmlDocument();
            xml.Load(path);

            XmlNode root = xml.SelectSingleNode("player-data");

            Dictionary<string, long> d = new Dictionary<string, long>();

            XmlNodeList xmlnds = root.ChildNodes;


            foreach (XmlNode v in xmlnds)
            {
                try
                {
                    d.Add(v.Name, long.Parse(v.InnerText));
                }
                catch (Exception ex){

                    Console.WriteLine("Error on loading save. Continuing with zero");
                    d.Add(v.Name, 0);

                }
            }

            return Player.LoadPlayer(d, root.Attributes.GetNamedItem("name").Value);
        }

    }
}
