// See https://aka.ms/new-console-template for more information
using FalloutMinigame.Objects;

/*
Player tomas = new Player("tomáš");
SaveSystem.SavePlayer(tomas);
*/
/*
Game myGame = new Game(tomas);
*/

//Console.WriteLine(SaveSystem.LoadPlayer("C:\\Users\\tomas\\source\\repos\\2022-p2a-prg-big-bad-project-TomasKelbasa\\FalloutMinigame\\FalloutMinigame\\Resource\\Test.xml"));
var s = SaveSystem.GetSaves();
Console.WriteLine("Loaded Saves: ");
foreach (var v in s)
{
    Console.WriteLine("[" + s.IndexOf(v) + "] - " + new FileInfo(v).Name.Replace(".xml",""));
}
/*
Game myGame = new Game(SaveSystem.LoadPlayer("./Resource/Saves/Test.xml"));
*/