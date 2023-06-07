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

        private static string SAVES_PATH = "./Resource/Saves/";

        public static List<string> GetSaves()
        {
            if (Directory.Exists(SAVES_PATH))
            {
                List<string> savesPaths = new List<string> ();
                foreach(var f in Directory.GetFiles(SAVES_PATH, "*.xml"))
                {
                    savesPaths.Add(f);

                }
                return savesPaths;
            }
            else
            {
                throw new DirectoryNotFoundException();
            }

    
        }

        public static int SavePlayer(Player p, string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement rootElement = xmlDoc.CreateElement("player-data");
            rootElement.SetAttribute("name", p.Name);
            xmlDoc.AppendChild(rootElement);
            foreach (var l in p.playerStats)
            {
                XmlElement el = xmlDoc.CreateElement(l.Key);
                el.InnerText = l.Value.ToString();
                rootElement.AppendChild(el);
            }
            try
            {
                xmlDoc.Save(path + p.Name + ".xml");
                return 1;
            }catch (Exception ex)
            {
                return -1;
            }
        }

        public static int SavePlayer(Player p)
        {
            return SavePlayer(p, SAVES_PATH);
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
