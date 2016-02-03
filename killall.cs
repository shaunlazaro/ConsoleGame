using System;
class Game
{
  static void Main()
  {
    Character player = new Character();
    CreatePlayer(ref player);
    LaunchGame(player);
  }
  static void CreatePlayer(ref Character player)
  {
    player.hp  = 10;
    player.atk = 1;
    player.def = 1;
    player.progress = 1;
    player.name = ChooseName();
  }
  static string ChooseName()
  {
    string name;
    ConsoleKeyInfo input = new ConsoleKeyInfo();

    string inputString;
    selectname:
    Console.Clear();
    Console.WriteLine("I need to know your name.");
    Console.Write("My name is: ");
    name = Console.ReadLine();
    confirmname:
    Console.Clear();
    Console.WriteLine("Your name is {0}?\n", name);
    Console.WriteLine("My name is {0} - Press Y\nMy name isn't {0} - Press N", name);
    input = Console.ReadKey(true);
    inputString = input.Key.ToString();
    inputString = inputString.ToLower();
    if(inputString == "y")
    {
      Console.Clear();
      Console.WriteLine("Hello, {0}!", name);
      Console.ReadKey(true);
    }
    else if (inputString == "n")
    {
      Console.Clear();
      Console.WriteLine("Then what is your name?");
      Console.ReadKey(true);
      goto selectname;
    }
    else
    {
      Console.Clear();
      Console.WriteLine("Please input the correct key.\n");
      Console.ReadKey(true);
      goto confirmname;
    }

    Console.Clear();
    return name;
  }

  static void LaunchGame(Character player)
  {
    player.PrintStats();
  }
}
