namespace FalloutMinigame.Objects
{
    public class Level
    {
        static private int _nextId = 0;
        static private int _defaultDifficulty = 0;
        static private int _numberOfLines = 15;
        static private string _symbols = "<>.-?!}{][/";
        static int GenerateId()
        {
            return _nextId++;
        }

        public int Id { get; private set; }
        public int Difficulty { get; private set; }
        public List<string> Words { get; private set; }
        public int WordsCount
        {
            get
            { return 8 + Difficulty * 2; }
            private set { }
        }
        private string _correctWord = "";
        public int RemainingAttempts { get; private set; }

        /// <summary>
        ///     Creates new object Level
        /// </summary>
        /// <param name="difficulty">Obtížnost (0 - nejlehčí, 5 - nejtěžší)</param>
        /// <param name="attempts">Počet pokusů</param>
        public Level(int difficulty, int attempts = 5)
        {
            if (difficulty >= 0 && difficulty <= 5) Difficulty = difficulty;
            else Difficulty = _defaultDifficulty;
            Id = GenerateId();
            Words = GenerateWords();
            _correctWord = Words[Random.Shared.Next(0, Words.Count)];
            if (attempts > 0)
            {
                RemainingAttempts = attempts;
            }
            else
            {
                RemainingAttempts = 5;
            }
        }

        /// <summary>
        ///     Vygeneruje seznam slov, které bude hráč hádat podle obtínosti. Čím vyšší obtížnost, tím delší slova a tím více slov.
        ///     Je zajištěno že slova nejsou stejná.
        /// </summary>
        /// <returns>Vrací seznam slov ve formátu List</returns>
        private List<string> GenerateWords()
        {
            List<string> words = new List<string>();
            String[] possibleWords = new string[] { };
            try
            {
                StreamReader reader = new StreamReader("./Resource/" + (Difficulty + 4).ToString() + "_letter.txt");
                possibleWords = reader.ReadToEnd().ToUpper().Split(",");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            while (words.Count < WordsCount)
            {
                int rnd = Random.Shared.Next(0, possibleWords.Length);
                if (!words.Contains(possibleWords[rnd]))
                {
                    words.Add(possibleWords[rnd]);
                }

            }

            return words;
        }
        /// <summary>
        ///     Generuje output připravený k zobrazení pro uživatele. Náhodné znaky čerpá z <see cref="_symbols"/>.
        /// </summary>
        /// <returns>Vrací output připravený k zobrazení rozdělený na jednotlivé řádky.</returns>
        public List<string> GenerateOutput()
        {
            List<string> output = new List<string>();
            int[] wordsPerLine = new int[_numberOfLines];
            for (int i = 0; i < wordsPerLine.Length; i++)
            {
                wordsPerLine[i] = 0;
            }
            while (wordsPerLine.Sum() < WordsCount)
            {
                int rnd = Random.Shared.Next(0, wordsPerLine.Count());
                if (wordsPerLine[rnd] < 3)
                {
                    wordsPerLine[rnd]++;
                }

            }
            string exampleLine = "";
            for (int j = 0; j < 50; j++)
            {
                exampleLine += "#";
            };
            int counter = 0;
            int c = Random.Shared.Next(17, 50);
            foreach (int i in wordsPerLine)
            {
                int o = i;
                string line = exampleLine.Substring(0);
                while (o > 0)
                {
                    int rnd = Random.Shared.Next(1, exampleLine.Length - Words[0].Length);
                    if (exampleLine.Contains(line.Substring(rnd - 1, Words[0].Length + 1)))
                    {
                        line = line.Substring(0, rnd) + Words[counter] + line.Substring(rnd + Words[0].Length);
                        counter++;
                        o--;
                    }
                }
                for (int q = 0; q < line.Length; q++)
                {
                    if (line[q].Equals('#'))
                    {
                        line = line.Substring(0, q) + _symbols[Random.Shared.Next(0, _symbols.Length)] + line.Substring(q + 1);
                    }

                }
                line = "0x" + (c++).ToString("X") + " " + line;
                output.Add(line);
            }

            return output;
        }

        /// <summary>
        ///     Porovnává se správným slovem. Pokud hráč zadá heslo které není na seznamu slov, tak se mu pokus neodečte.
        /// </summary>
        /// <param name="input">Hráčem zadaný vstup</param>
        /// <returns>Vrací počet znaků, které jsou na správném místě. Pokud jsou všechny správně tak vrací hodnotu 100. V případě, že hráč se pokusí zadat slovo, které si vymyslel, vrátí program -1.</returns>
        public int Guess(string input)
        {
            input = input.ToUpper();
            int count = 0;
            if (input.Length == _correctWord.Length && Words.Contains(input) && RemainingAttempts > 0)
            {
                for (int i = 0; i < _correctWord.Length; i++)
                {
                    if (_correctWord[i].Equals(input[i]))
                    {
                        count++;
                    }
                }
                if (count == _correctWord.Length)
                {
                    return 100;
                }
                else
                {
                    RemainingAttempts--;
                    return count;
                }
            }
            else
            {
                return -1;
            }
        }

    }
}
