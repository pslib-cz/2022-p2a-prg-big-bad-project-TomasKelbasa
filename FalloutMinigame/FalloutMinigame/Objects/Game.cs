using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FalloutMinigame.Objects
{
    internal class Game
    {

        public Player currentPlayer {  get; private set; }

        public Game(Player player)
        {
            currentPlayer = player;
            Menu();
        }

        private static string ReadStringInput()
        {
            Console.Write("> ");
            string input = Console.ReadLine();
            input = input.ToUpper();
            input = input.Trim();
            return input;
        }

        public void NewLevel(int difficulty, int attempts = 5)
        {
            Console.Clear();
            Level newlvl = new Level(difficulty, attempts);
            foreach(var line in newlvl.GenerateOutput())
            {
                Console.WriteLine(line.ToString());
            }
        }

        private void Menu()
        {
            Console.Clear();
            try{
                StreamReader s = new StreamReader("./Resource/Logo.txt");
                Console.WriteLine(s.ReadToEnd());
            }catch(FileNotFoundException ex)
            {
                Console.WriteLine("Logo.txt file not found.");
            }
            Console.WriteLine("[1] - New level\n[2] - Stats\n[3] - Save & Exit");
            var input = "";
            do
            {
                input = ReadStringInput();
                if(input.Equals("1") || input.Equals("2") || input.Equals("3"))
                {
                    break;
                }
            }while (true);
            switch (input)
            {
                case "1":
                    NewLevel(2);
                    break;
                case "2":
                    Stats();
                    break;
                case "3":
                    break;
            }
        }

        private void Stats()
        {
            Console.Clear();
            Console.WriteLine("Statiska");
            Console.WriteLine("Press anything to return to main menu");
            ReadStringInput();
            Menu();
        }

        private void LostLevel()
        {
            //currentPlayer.LostLevels++;
            Console.WriteLine("Terminal has been overloaded.");
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("Terminal is temporarily locked. Please wait.");
            Thread.Sleep(10000 - currentPlayer.TimeBonus);
        }

    }
}
