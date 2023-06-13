// See https://aka.ms/new-console-template for more information
using FalloutMinigame.Objects;
using System.Text.RegularExpressions;


// args[0] - složka pro načítání a ukládání savů
if (args.Length > 0)
{
    if (Directory.Exists(args[0])) SaveSystem.SAVES_PATH = args[0];
}



var saves = SaveSystem.GetSaves();

Console.WriteLine("[N] New save");

foreach (var save in saves)
{
    Console.WriteLine("[{0}] {1}", saves.IndexOf(save), new FileInfo(save).Name.Replace(".xml", ""));
}


while (true)
{
    string i = Console.ReadLine();
    if (i != null && Regex.IsMatch(i, @"^\d+$"))
    {
        int ix = int.Parse(i);
        if (ix >= 0 && ix < saves.Count)
        {
            Game game = new Game(SaveSystem.LoadPlayer(saves[ix]));
            break;
        }

    }
    else if (i.ToUpper().Trim().Equals("N"))
    {
        while (true)
        {
            Console.WriteLine("Type a name: ");
            string s = Console.ReadLine();
            if (s.IndexOfAny(Path.GetInvalidFileNameChars()) < 0 && !File.Exists(Path.Combine(SaveSystem.SAVES_PATH, s, ".xml")))
            {
                Console.WriteLine("OK");
                Game game = new Game(new Player(s));
                break;
            }
            else
            {
                Console.WriteLine("Invalid name");
            }
        }
        break;
    }

}