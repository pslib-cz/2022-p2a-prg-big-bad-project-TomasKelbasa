using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FalloutMinigame.Objects
{
    internal class Game
    {
        static private int _nextId = 0;
        static private int _defaultDifficulty = 0;
        static private string _symbols = "<>.-?!{}[]/";
        static int GenerateId()
        {
            return _nextId++;
        }

        public int Id { get;private set; }
        public int Difficulty { get; private set; }
        public List<string> Words { get; private set; }
        public int WordsCount { get 
            { return 8 + Difficulty * 2; }
            private set { } }

        /// <summary>
        ///     Creates new object Game
        /// </summary>
        /// <param name="difficulty">Obtížnost (0 - nejlehčí, 5 - nejtěžší)</param>
        public Game(int difficulty)
        {
            if(difficulty >= 0 && difficulty <= 5) Difficulty = difficulty;
            else Difficulty = _defaultDifficulty;
            Id = GenerateId();
            Words = GenerateWords();
            Console.WriteLine(string.Join("\n", Words));
        }

        /// <summary>
        ///     Vygeneruje seznam slov, které bude hráč hádat podle obtínosti. Čím vyšší obtížnost, tím delší slova a tím více slov.
        /// </summary>
        /// <returns>Vrací seznam slov ve formátu List</returns>
        private List<string> GenerateWords()
        {
            List<string> words = new List<string>();
            String[] possibleWords = new string[] {};
            try
            {
                StreamReader reader = new StreamReader("../../../Resource/" + (Difficulty + 4).ToString() + "_letter.txt");
                possibleWords = reader.ReadToEnd().Split(",");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            while(words.Count < WordsCount)
            {
                int rnd = Random.Shared.Next(0, possibleWords.Length);
                if (!words.Contains(possibleWords[rnd]))
                {
                    words.Add(possibleWords[rnd]);
                }

            }

            return words;
        }

        public List<string> GenerateOutput()
        {
            string output = String.Empty;
            int[] wordsPerLine = new int[10];
            for(int i = 0; i < wordsPerLine.Length; i++)
            {
                wordsPerLine[i] = 0;
            }
            while(wordsPerLine.Sum() < WordsCount)
            {
                int rnd = Random.Shared.Next(0, wordsPerLine.Count());
                if (wordsPerLine[rnd] < 3)
                {
                    wordsPerLine[rnd]++;
                }

            }

            return new List<string> { String.Join(",", wordsPerLine) };
        }

    }
}
