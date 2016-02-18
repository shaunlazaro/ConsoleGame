using System;
using System.IO;


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
    Random rng = new Random();// Used to do any rng.
    Monster enemy = new Monster();
    //Game Loop.
    while(true)
    {
      CreateMonster(player, rng.Next( 1, 4 ), out enemy);
      
      // The combat method will return true if the player wins,
      // or false if the player dies.
      if(Combat(out player, out enemy))
      {
      Console.Clear();
      Console.WriteLine("After that encounter, you continue on");
      Console.ReadKey(true);
      player.progress++;
      }
      else
      {
        Console.Clear();
        Console.WriteLine("You died");
        player.PrintStats();
        while(true);
      }
    }
    //Should never happen, unless testing.
    Console.WriteLine("Program Ending");
    Console.ReadKey(true);
  }

  // The CreateMonster method accepts a scenario from 1-3, then generates a monster with stats.
  // The method will also give a brief description to player before combat starts.
  static void CreateMonster(Character player, int scenario, out Monster enemy)
  {
    enemy.hp = player.hp;
    enemy.atk = player.atk;
    enemy.def = player.def;
    if(scenario == 1)
    {
      enemy.hp += player.hp * 0.5;
      enemy.def = player.def * 0.5;
      Console.WriteLine("As you progress, the growls tell you that you are in claimed land...");
    }
    else if (scenario == 2)
    {
      enemy.atk += player.atk;
      Console.WriteLine("You hear the roar of an angry beast...");
    }
    else if (scenario == 3)
    {
      enemy.hp = enemy.hp / 2;
      enemy.atk = enemy.atk / 2;
      enemy.def = enemy.def / 2;
      Console.WriteLine("The creature in front of you cowers in fear, but won't let you progress.");
    }

    Console.ReadKey(true);
  }

  static bool Combat(out Character player, out Monster enemy)
  {
    currentPlayerHp = player.hp;
    while(currentPlayerHp > 1 && enemy.hp > 1)
    {

    }
    bool playerWin = false;
    if(currentPlayerHp > 0)
    {
      playerWin = true;
    }
    return playerWin;
  }

}
