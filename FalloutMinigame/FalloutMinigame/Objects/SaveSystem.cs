using System;
using System.Collections.Generic;
using System.Linq;
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

        public int SavePlayer(Player p)
        {
            string filePath = "Cesta_k_XML_souboru.xml";

            StreamWriter xmlWriter = new StreamWriter("./Resource/Saves/" + p.Name + ".xml");

            

            return 0;
        }

    }
}
