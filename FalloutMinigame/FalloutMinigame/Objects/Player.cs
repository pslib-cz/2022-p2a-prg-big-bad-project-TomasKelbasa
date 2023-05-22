using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutMinigame.Objects
{

    internal class Player
    {

        public int XP { get; private set; }
        public int Level { get; private set; }

        public string Name { get; private set; }
        public int TimeBonus { get; private set; }

        public Player(string name) {
        
            Name = name;
            XP = 0;
            Level = 0;

        }

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

    }
}
