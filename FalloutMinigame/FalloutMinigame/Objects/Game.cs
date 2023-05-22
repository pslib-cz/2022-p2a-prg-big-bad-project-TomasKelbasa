using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FalloutMinigame.Objects
{
    internal class Game
    {

        public Player currentPlayer { get; private set; }

        public Game(Player player)
        {
            currentPlayer = player;
            Menu();
        }

        private static string ReadStringInput()
        {
            Console.Write("> ");
            string input = "" + Console.ReadLine();
            input = input.ToUpper();
            input = input.Trim();
            return input;
        }

        public void NewLevel(int attempts = 5)
        {
            Console.Clear();
            Console.WriteLine("Choose a difficulty:\n[0] - Novice\n[1] - Advanced\n[2] - Expert\n[3] - Master\n[4] - Grandmaster\n[5] - Mr. PY\n");
            int difficulty = -1;
            bool converted = false;
            do
            {
                string s = ReadStringInput();
                if(Regex.IsMatch(s, "^[0-5]$"))
                {
                    converted = Int32.TryParse(s, out difficulty);
                }

                //Console.WriteLine("Converted: " + difficulty);

            } while (!converted);
            Level newlvl = new Level(difficulty, attempts);
            Console.WriteLine("OK");
            Console.WriteLine(newlvl.RemainingAttempts);
            Thread.Sleep(500);
            Console.Clear();
            //vypíše output postupně - aby to bylo vizuálně hezké
            foreach (var line in newlvl.GenerateOutput())
            {
                foreach(var item in line)
                {
                    Console.Write(item);
                    Thread.Sleep(3);
                }
                Console.Write("\n");
            }

            bool won = false;

            while(newlvl.RemainingAttempts > 0 && !won)
            {
                string input = ReadStringInput();
                int output = newlvl.Guess(input);
                switch (output)
                {
                    case -1: Console.WriteLine("ERORR");
                        break;
                    case 100: Console.WriteLine("ACESS GRANTED");
                        won = true;
                        WonLevel(newlvl.Difficulty, newlvl.RemainingAttempts);
                        break;
                    default: Console.WriteLine("SIMILARITY: " + output + " REMAINING ATTEMPTS: " + newlvl.RemainingAttempts);
                        break;
                }
            }
            if(newlvl.RemainingAttempts <= 0)
            {
                LostLevel();
            }
        }

        private void Menu()
        {
            Console.Clear();
            try {

                Console.WriteLine(new StreamReader("./Resource/Logo.txt").ReadToEnd());

            } catch (FileNotFoundException ex)
            {
                Console.WriteLine("Logo.txt file not found.");
            }
            Console.WriteLine("\nPlayer: {0}\n", currentPlayer.Name);
            Console.WriteLine("[0] - Help\n[1] - New level\n[2] - Stats\n[3] - Save & Exit\n");
            var input = "";
            do
            {
                input = ReadStringInput();
                if (Regex.IsMatch(input, "^[0-3]$"))break;

            } while (true);
            switch (input)
            {
                case "0":
                    Help();
                    break;
                case "1":
                    NewLevel();
                    break;
                case "2":
                    Stats();
                    break;
                case "3":
                    break;
            }
        }
        private void Help()
        {
            Console.Clear();
            Console.WriteLine("Vítej ve hře Fallout Hacking!\nTvým úkolem je zde prolamovat terminály. Za úspěšné prolomení terminálu budeš odměněn XP, za neúspěšné budeš časově penalyzován.");
            Console.WriteLine("Jak ale funguje nabourávání terminálů? Skvělá otázka. Máš vždy pouze pár pokusů na to uhodnout správné heslo. Heslo je vždy jedno ze slov, které uvidíš. Naštěstí pokud zvolíš nesprávné heslo, tak se dozvíš kolik znaků je správných.");
            Console.WriteLine("\nPříklad - správným heslem je slovo HUMAN avšak hráč zvolil slovo CURVE. Počítač mu řekne že pouze jeden znak je správný (jedná se o písmenu U). Poté hráč zvolil slovo MOTOR, na to mu počítač odpověděl nulou (žádný znak není správně).");
            Console.WriteLine("\nBohužel když hráči dojdou pokusy, tak prohrál a musí počkat několik sekund než bude moct znovu hrát.");
            Console.WriteLine("\nTerminály mají také několik stupňů obtížnosti - čím vyšší obtížnost, tím více slov, tím delší slova a tím více XP za úspěšné prolomení terminálu");
            Console.WriteLine("\nPress Enter to return to main menu");
            ReadStringInput();
            Menu();
        }
        private void Stats()
        {
            Console.Clear();
            Console.WriteLine("Statiska");
            Console.WriteLine("Press Enter to return to main menu");
            ReadStringInput();
            Menu();
        }

        private void WonLevel(int levelDifficulty, int remainingAttempts) 
        {
            int xprewarded = (levelDifficulty + 3) * Random.Shared.Next(4, 8) + remainingAttempts * 2;
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("Terminal has been succesfully hacked!");
            Console.WriteLine("You gain +" + xprewarded + " XP");
            currentPlayer.AddXP(xprewarded);
            Console.ReadLine();
            Menu();
        }

        private void LostLevel()
        {
            //currentPlayer.LostLevels++;
            Console.Clear();
            Console.WriteLine("Terminal has been overloaded.");
            Console.WriteLine("Please wait.");
            for(int i = 0; i < 10000 - currentPlayer.TimeBonus; i += 100)
            {
                Console.Write("#");
                Thread.Sleep(100);
            }
            Console.WriteLine("\n\nSystem reboot");
            Thread.Sleep(500);
            Menu();
        }

    }
}
