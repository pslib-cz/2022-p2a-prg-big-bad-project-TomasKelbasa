using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutMinigame.Objects
{

    internal class Player
    {
        /// <summary>
        ///     Zkušenosti
        /// </summary>
        public int XP { get; private set; }

        /// <summary>
        ///     Level hráče - za každý nový level je hráč odměněn perkpointem
        /// </summary>
        public int Level { get; private set; }

        /// <summary>
        ///     Jméno hráče
        /// </summary>
        public string Name { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public int TimeBonus { get; private set; } = 0;

        /// <summary>
        ///     Počet prohraných levelů
        /// </summary>
        public int LostLevels { get; set; } = 0;

        /// <summary>
        ///     Počet prohraných levelů
        /// </summary>
        public int WonLevels {get; set;} = 0;

        public Player(string name) {
        
            CreatedAt = DateTime.Now;
            Name = name;
            XP = 0;
            Level = 0;

        }

        /// <summary>
        ///     Přidá hráči XP a pokud dosáhne nového levelu tak vyvolá metodu LevelUp()
        /// </summary>
        /// <param name="xp">počet XP k přidání</param>
        /// <returns>Aktuální level hráče po přidání XP</returns>
        public int AddXP(int xp)
        {
            XP += xp;
            int oldLvl = Level;
            Level = XP / (90 + XP / 20);
            if (Level > oldLvl)
            {
                for(int i = 0; i < Level - oldLvl;  i++)
                {
                    LevelUp();
                }
            }
            return Level;
        }

        public void LevelUp()
        {
            Console.Clear();
            Console.WriteLine("LEVEL UP!");
            Console.WriteLine("Choose a new perk");

        }

        public string GetStats()
        {
            string stats = string.Empty;
            stats += "Name: " + Name;
            stats += "\nCreated at: " + CreatedAt;
            stats += "\nLevel: " + Level;
            stats += "\nXP: " + XP;
            stats += "\nLevelsPlayed: " + (WonLevels + LostLevels);
            stats += "\nWonLevels: " + WonLevels;
            stats += "\nLostLevels: " + LostLevels;

            return stats;
        }

    }
}
