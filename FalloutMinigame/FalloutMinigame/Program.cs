// See https://aka.ms/new-console-template for more information
using FalloutMinigame.Objects;

Level test = new Level(5);
foreach(var o in test.GenerateOutput())
{
    Console.WriteLine(o);
}