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
            xmlDoc.AppendChild(rootElement);
            AppendString(ref xmlDoc, ref rootElement, "Level", p.Level.ToString());
            AppendString(ref xmlDoc, ref rootElement, "XP", p.XP.ToString());
            AppendString(ref xmlDoc, ref rootElement, "LostLevels", p.LostLevels.ToString());
            AppendString(ref xmlDoc, ref rootElement, "WonLevels", p.WonLevels.ToString());
            xmlDoc.Save(saveDirectory + p.Name + ".xml");

            return 1;
        }

        public static void AppendString(ref XmlDocument d, ref XmlElement root, string name, string value)
        {
            XmlElement el = d.CreateElement(name);
            el.InnerText = value;
            root.AppendChild(el);
        }


        public static Player LoadPlayer(string path)
        {
            if(!File.Exists(path)) throw new FileNotFoundException();
            XmlDocument xml = new XmlDocument();
            xml.Load(path);


            return Player.LoadPlayer(ReadStringXML(xml, "Name"), ReadIntXML(xml, "XP"), ReadIntXML(xml, "Level"), ReadIntXML(xml,"LostLevels"), ReadIntXML(xml,"WonLevels"), DateTime.FromBinary(long.Parse(ReadStringXML(xml, "CreatedAt"))), ReadIntXML(xml, "TimeBonus"));
        }

        public static string ReadStringXML(XmlDocument xml, string name)
        {
            return xml.SelectSingleNode("player-data/" + name).InnerText;
        }

        public static int ReadIntXML(XmlDocument xml, string name)
        {
            return int.Parse(ReadStringXML(xml,name));
        }

    }
}
