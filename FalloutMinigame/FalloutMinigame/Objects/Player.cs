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
        ///     Jméno hráče
        /// </summary>
        public string Name { get; private set; }

        public Dictionary<string, long> playerStats { get; private set; } = new Dictionary<string, long>();
        public Player(string name) {

            Name = name;
            playerStats.Add("CreatedAt", DateTime.Now.ToBinary());
            playerStats.Add("XP", 0);
            playerStats.Add("Level", 0);
            playerStats.Add("WonGames", 0);
            playerStats.Add("LostGames", 0);
            playerStats.Add("TimeBonus", 0);

        }

        /// <summary>
        ///     Working probably
        /// </summary>
        /// <param name="data"></param>
        /// <param name="name">Jméno hráče</param>
        /// <returns></returns>
        public static Player LoadPlayer(Dictionary<string, long> data, string name)
        {
            Player player = new Player(name);

            player.playerStats = data;
            player.Name = name;

            Console.WriteLine("Player loaded: " + player.Name);

            return player;
        }
        
        /// <summary>
        ///     Přidá hráči XP a pokud dosáhne nového levelu tak vyvolá metodu LevelUp()
        /// </summary>
        /// <param name="xp">počet XP k přidání</param>
        /// <returns>Aktuální level hráče po přidání XP</returns>
        public long AddXP(int xp)
        {
            playerStats["XP"] += xp;
            long oldLvl = playerStats["Level"];
            playerStats["Level"] = playerStats["XP"] / (90 + playerStats["XP"] / 20);
            if (playerStats["Level"] > oldLvl)
            {
                for(int i = 0; i < playerStats["Level"] - oldLvl;  i++)
                {
                    LevelUp();
                }
            }
            return playerStats["Level"];
        }
        //TODO
        public void LevelUp()
        {
            Console.Clear();
            Console.WriteLine("LEVEL UP!");
            Console.WriteLine("Choose a new perk");

        }

        public string GetStats()
        {
            string output = string.Empty;

            output += "Name: " + Name;

            foreach(var stat in playerStats) output += "\n" + stat.Key + ": " + stat.Value;

            output += "\n" + "CreatedAt (formated): " + DateTime.FromBinary(playerStats["CreatedAt"]).ToString();

            return output;
        }
        
    }
}
