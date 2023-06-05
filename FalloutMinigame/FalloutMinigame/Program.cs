// See https://aka.ms/new-console-template for more information
using FalloutMinigame.Objects;


Player tomas = new Player("tomáš");
SaveSystem.SavePlayer(tomas);
/*
Game myGame = new Game(tomas);
*/

//Console.WriteLine(SaveSystem.LoadPlayer("C:\\Users\\tomas\\source\\repos\\2022-p2a-prg-big-bad-project-TomasKelbasa\\FalloutMinigame\\FalloutMinigame\\Resource\\Test.xml"));



Game g = new Game(SaveSystem.LoadPlayer("C:\\Users\\tomas\\source\\repos\\2022-p2a-prg-big-bad-project-TomasKelbasa\\FalloutMinigame\\FalloutMinigame\\Resource\\Test.xml"));
