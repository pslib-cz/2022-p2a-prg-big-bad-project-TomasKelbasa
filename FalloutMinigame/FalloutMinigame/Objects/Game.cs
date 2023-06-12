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
        /// <summary>
        ///     Aktuální hráč hrající hru
        /// </summary>
        public Player currentPlayer { get; private set; }

        private const ConsoleColor consoleNormal = ConsoleColor.White;
        private const ConsoleColor consoleHighlight = ConsoleColor.Red;
        private const ConsoleColor consoleUser = ConsoleColor.Blue;

        public Game(Player player)
        {
            currentPlayer = player;
            Console.ForegroundColor = consoleNormal;
            Menu();
        }

        /// <summary>
        ///     Čte vstup od uživatele z konzole a zároveň ho mírně zformátuje
        /// </summary>
        /// <returns>Zformátovaný vstup od uživatele</returns>

        private static string ReadStringInput()
        {
            Console.ForegroundColor = consoleUser;
            Console.Write("> ");
            string input = "" + Console.ReadLine();
            input = input.ToUpper();
            input = input.Trim();
            Console.ForegroundColor = consoleNormal;
            return input;
        }


        /// <summary>
        ///     Vytváří nový level o obtížnosti zadané uživatelem. Končí buď zavoláním metody LostGame() nebo WonGame().
        /// </summary>
        /// <param name="attempts">Počet pokusů které bude uživatel mít na daný level</param>
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
            Thread.Sleep(500);
            Console.Clear();
            //vypíše output postupně - aby to bylo vizuálně hezké
            bool WAIT = true;
            foreach (var line in newlvl.GenerateOutput())
            {
                for(int i = 0; i < line.Length; i++)
                {
                    if (i > 5 && line[i] >= 65 && line[i] <= 90) 
                    { 
                        Console.ForegroundColor = consoleHighlight;
                        Console.Write(line[i]);
                        Console.ForegroundColor = consoleNormal;
                    }
                    else
                    {
                        Console.Write(line[i]);
                    }

                   if(WAIT) Thread.Sleep(1);
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
                    case 100: Console.WriteLine("ACCESS GRANTED");
                        won = true;
                        WonLevel(newlvl.Difficulty, newlvl.RemainingAttempts);
                        break;
                    default: Console.WriteLine("SIMILARITY: " + output + "; REMAINING ATTEMPTS: " + newlvl.RemainingAttempts);
                        break;
                }
            }
            if(newlvl.RemainingAttempts <= 0)
            {
                LostLevel();
            }
        }


        /// <summary>
        ///     Hlavní menu nabídka - stará se i o čtení inputu od uživatele
        /// </summary>
        private void Menu()
        {
            Console.Clear();
            try {

                Console.WriteLine(new StreamReader("./Resource/Logo.txt").ReadToEnd());

            } catch (FileNotFoundException ex)
            {
                Console.WriteLine("Logo.txt file not found.");
                Console.WriteLine(ex);
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
                    SaveExit();
                    break;
            }
        }

        /// <summary>
        ///     Vypíše uživateli nápovědu jak hru hrát
        /// </summary>
        private void Help()
        {
            Console.Clear();
            Console.WriteLine("Vítej ve hře Fallout Hacking!\n\nTvým úkolem je zde prolamovat terminály. Za úspěšné prolomení terminálu budeš odměněn XP, za neúspěšné budeš časově penalyzován.");
            Console.WriteLine("Jak ale funguje nabourávání terminálů? Skvělá otázka. Máš vždy pouze pár pokusů na to uhodnout správné heslo. Heslo je vždy jedno ze slov, které uvidíš. Naštěstí pokud zvolíš nesprávné heslo, tak se dozvíš kolik znaků je správných.");
            Console.WriteLine("\nPříklad - správným heslem je slovo HUMAN avšak hráč zvolil slovo CURVE. Počítač mu řekne že pouze jeden znak je správný (jedná se o písmenu U). Poté hráč zvolil slovo MOTOR, na to mu počítač odpověděl nulou (žádný znak není správně).");
            Console.WriteLine("\nBohužel když hráči dojdou pokusy, tak prohrál a musí počkat několik sekund než bude moct znovu hrát.");
            Console.WriteLine("\nTerminály mají také několik stupňů obtížnosti - čím vyšší obtížnost, tím více slov, tím delší slova a tím více XP za úspěšné prolomení terminálu");
            Console.WriteLine("\nPress Enter to return to main menu");
            ReadStringInput();
            Menu();
        }

        /// <summary>
        ///     Zobrazí statisky o aktuálním hráči
        /// </summary>
        private void Stats()
        {
            Console.Clear();
            Console.WriteLine("Stats\n");
            Console.WriteLine(currentPlayer.GetStats());
            Console.WriteLine("\nPress Enter to return to main menu");
            ReadStringInput();
            Menu();
        }

        private void SaveExit()
        {
            Console.Clear();
            Console.WriteLine("Saving...");
            Thread.Sleep(500);
            SaveSystem.SavePlayer(currentPlayer);
            Console.WriteLine("Success");
            ReadStringInput();
        }

        /// <summary>
        ///     Vyhodnocení vyhraného levelu (udělení XP)
        /// </summary>
        /// <param name="levelDifficulty">Obtížnost levelu (0-5)</param>
        /// <param name="remainingAttempts">Počet zbylých pokusů daného leveluj</param>
        private void WonLevel(int levelDifficulty, int remainingAttempts) 
        {
            currentPlayer.playerStats["WonLevels"]++;
            int xprewarded = (levelDifficulty + 3) * Random.Shared.Next(4, 8) + remainingAttempts * 2;
            Thread.Sleep(500);
            Console.Clear();
            Console.WriteLine("Terminal has been succesfully hacked!");
            Console.WriteLine("You gain +" + xprewarded + " XP");
            currentPlayer.AddXP(xprewarded);
            ReadStringInput();
            Menu();
        }


        /// <summary>
        ///     Vyhodnocení prohraného levelu
        /// </summary>
        private void LostLevel()
        {
            currentPlayer.playerStats["LostLevels"]++;
            Console.Clear();
            Console.WriteLine("Terminal has been overloaded.");
            Console.WriteLine("Please wait.\n");

            // LOADBAR
            Console.CursorVisible = false;
            for(int i = 0; i < 50/* - currentPlayer.TimeBonus*/; i++)
            {
                string output = "[";
                for(int j = 0; j < i; j++)
                {
                    output += "#";
                }
                for(int k = 0; k < (49 - i); k++)
                {
                    output += " ";
                }
                output += "]";
                Console.Write(output);
                Thread.Sleep(200);
                string del = "";
                for(int l = 0; l < 52; l++)
                {
                    del += "\b";
                }
                Console.Write(del);
            }
            Console.CursorVisible = true;
            Console.WriteLine("\n\nSystem reboot");
            Thread.Sleep(500);
            Menu();
        }

    }
}
