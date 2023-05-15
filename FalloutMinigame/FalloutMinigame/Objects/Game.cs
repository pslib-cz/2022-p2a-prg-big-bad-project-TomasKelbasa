using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
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
            string input = Console.ReadLine();
            input = input.ToUpper();
            input = input.Trim();
            return input;
        }

        public void NewLevel(int difficulty, int attempts = 5)
        {
            Console.Clear();
            Level newlvl = new Level(difficulty, attempts);
            foreach (var line in newlvl.GenerateOutput())
            {
                Console.WriteLine(line.ToString());
            }
        }

        private void Menu()
        {
            Console.Clear();
            try {
                StreamReader s = new StreamReader("./Resource/Logo.txt");
                Console.WriteLine(s.ReadToEnd());
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
                if (input.Equals("0") || input.Equals("1") || input.Equals("2") || input.Equals("3"))
                {
                    break;
                }
            } while (true);
            switch (input)
            {
                case "0":
                    Help();
                    break;
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
        private void Help()
        {
            Console.Clear();
            Console.WriteLine("Vítej ve hře Fallout Hacking!\nTvým úkolem je zde prolamovat terminály. Za úspěšné prolomení terminálu budeš odměněn XP, za neúspěšné budeš časově penalyzován.");
            Console.WriteLine("Jak ale funguje nabourávání terminálů? Skvělá otázka. Máš vždy pouze pár pokusů na to uhodnout správné heslo. Heslo je vždy jedno ze slov, které uvidíš. Naštěstí pokud zvolíš nesprávné heslo, tak se dozvíš kolik znaků je správných.");
            Console.WriteLine("\nPříklad - správným heslem je slovo HUMAN avšak hráč zvolil slovo CURVE. Počítač mu řekne že pouze jeden znak je správný (jedná se o písmenu U). Poté hráč zvolil slovo MOTOR, na to mu počítač odpověděl nulou (žádný znak není správně).");
            Console.WriteLine("\nBohužel když hráči dojdou pokusy, tak prohrál a musí počkat několik sekund než bude moct znovu hrát.");
            Console.WriteLine("\nTerminály mají několik stupňů obtížnosti - čím vyšší obtížnost, tím více slov, tím delší slova a tím více XP za úspěšné prolomení terminálu");
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
