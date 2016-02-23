using System;
using System.IO;


class Game
{
  static void Main()
  {
    Character player = new Character();
    CreatePlayer(ref player);
    TitleScreen();
    LaunchGame(player);
  }
  static void CreatePlayer(ref Character player)
  {
    player.hp  = 100;
    player.atk = 10;
    player.def = 2;
    player.progress = 1;
    player.name = ChooseName();
  }
  static void TitleScreen()
  {    
    Console.Clear();
    title:
    Console.WriteLine("KILLALL\n\n");
    Console.WriteLine("Launch Game  - Press A");
    Console.WriteLine("Instructions - Press S");
    Console.WriteLine("Credits      - Press D");
   
    string inputString = "ssss";
    ConsoleKeyInfo input = new ConsoleKeyInfo();

    input = Console.ReadKey(true);
    inputString = input.Key.ToString();
    inputString = inputString.ToLower();

    if(inputString == "s")
    {
      Console.Clear();
      Instructions();
    }
    else if(inputString == "d")
    {
      Console.Clear();
      Credits();
    }
    else if(inputString == "a")
    {
      Console.Clear();
    }
    else
    {
      goto title;
    }
  }

  static void Instructions()
  {
    Console.WriteLine("In the KillAll game, your goal is to get as far as possible...\n");
    Console.ReadKey(true);
    Console.WriteLine("Before you die");
    Console.ReadKey(true);
    Console.WriteLine("You will fight monsters");
    Console.ReadKey(true);
    Console.WriteLine("Your blunt force attack hits the enemy hard, scaling off of your attack well");
    Console.WriteLine("Your piercing attack hits the enemy weakly, but ignores enemy defense.");
    Console.ReadKey(true);
    Console.Clear();

    TitleScreen();
  }
  static void Credits()
  {
    Console.WriteLine("EVERYTHING BY SHAUN LAZARO XG");
    Console.ReadKey(true);
    Console.Clear();
    TitleScreen();
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
      WriteRed("Then what is your name?");
      Console.ReadKey(true);
      goto selectname;
    }
    else
    {
      Console.Clear();
      WriteRed("Please input the correct key.\n");
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
      CreateMonster(player, rng.Next( 1, 4 ), ref enemy);
      
      // The combat method will return true if the player wins,
      // or false if the player dies.
      if(Combat(ref player, enemy))
      {
      Console.Clear();
      Console.WriteLine("After that encounter, you continue on...");
      Console.ReadKey(true);
      player.progress++;
      }
      else
      {
        Console.Clear();
        WriteRed("You died");
        player.PrintStats();
        while(true);
      }
    }
  }

  // The CreateMonster method accepts a scenario from 1-3, then generates a monster with stats.
  // The method will also give a brief description to player before combat starts.
  static void CreateMonster(Character player, int scenario, ref Monster enemy)
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

    enemy.PrintStats();
    Console.ReadKey(true);
    Console.Clear();
  }

  static bool Combat(ref Character player, Monster enemy)
  {
    double currentPlayerHp = player.hp;
    while(currentPlayerHp > 1 && enemy.hp > 1)
    {
      ConsoleKeyInfo keyPrompt = CombatPrompt(player, enemy);
      Console.Clear();
      if(keyPrompt.Key.ToString().ToLower() == "a")
      {
        Console.WriteLine("You hit the enemy hard, but the monster blocks some damage.");
        enemy.hp -= player.CharacterAttack("strong", player, enemy);
      }
      else if (keyPrompt.Key.ToString().ToLower() == "s")
      {
        Console.WriteLine("You aim, and strike at the monster's weak point.");
        enemy.hp -= player.CharacterAttack("pierce", player, enemy);
      }
      else
      {
        WriteRed("Welp, Game Broke.");
      }
      Console.ReadKey(true);

      Console.WriteLine("The enemy returns with an attack of its own!");
      currentPlayerHp -= enemy.EnemyBasic(player, enemy);
    }
    Console.WriteLine("The monster has been slain.");
    Console.ReadKey();
    bool playerWin = false;
    if(currentPlayerHp > 0)
    {
      playerWin = true;
    }
    return playerWin;
  }

  static ConsoleKeyInfo CombatPrompt(Character player, Monster enemy)
  {
    ConsoleKeyInfo playerPress = new ConsoleKeyInfo();
    Console.Clear();
    promptAction:
    player.PrintStats();
    enemy.PrintStats();
    Console.WriteLine("What would you like to do?");
    Console.WriteLine("\n Blunt Force Attack - A\n Piercing Attack - S ");
    playerPress = Console.ReadKey(true);
    string playerPressString = playerPress.Key.ToString();
    playerPressString = playerPressString.ToLower();
    if(playerPressString != "a" && playerPressString != "s")
    {
      Console.Clear();
      WriteRed("Please press A or S\n");
      Console.ReadKey(true);
      goto promptAction;      
    }
    return playerPress;
  }

  // Used For Errors.
  static void WriteRed(string s)
  {
    Console.BackgroundColor = ConsoleColor.White;
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(s);
    Console.ResetColor();
  }

  // Used for narration parts.
  static void WriteNarration(string s)
  {
    //WIP
  }
}
