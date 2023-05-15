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

        public string Name { get; private set; }
        public int TimeBonus { get; private set; }

        public Player(string name) {
        
            Name = name;
            XP = 0;

        }

    }
}
