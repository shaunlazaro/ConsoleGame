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

  //Save Format:
  //HP
  //ATK
  //DEF
  //Progress
  static void CreatePlayer(ref Character player)
  {
    string fileLocation = @"save.txt";
    player.hp  = 50;
    player.mp  = 5;
    player.atk = 5;
    player.def = 2;
    player.progress = 1;
    player.initialProgress = 0;
    player.freeKill = true;

    if(File.Exists(fileLocation))
    {
      StreamReader x = new StreamReader(fileLocation);
      using (x)
      {
        int playerProgress = int.Parse(x.ReadLine());
        player.hp  += playerProgress * 2;
        player.mp  += playerProgress / 2;
        player.atk += playerProgress;
        player.def += playerProgress / 2;
        player.initialProgress = playerProgress;
        player.name = x.ReadLine();
      }
      Console.Clear();
      Console.WriteLine("You feel the power of your ancestors passing through your body.");
      Console.ReadKey(true);
    }
    else
    {
      string playerClass = ChooseClass();
      player.name = ChooseName();

    }
  }
  static void TitleScreen()
  {    
    Console.Clear();
    title:
    Console.WriteLine("KILLALL\n\n");
    Console.WriteLine("Launch Game  - Press A");
    Console.WriteLine("Instructions - Press S");
    Console.WriteLine("Credits      - Press D");
    if(File.Exists(@"save.txt"))
    {
    Console.WriteLine("\nDelete Save- Press F");
    }
    string inputString = "ssss";
    ConsoleKeyInfo input = new ConsoleKeyInfo();

    input = Console.ReadKey(true);
    inputString = input.Key.ToString();
    inputString = inputString.ToLower();

    if(File.Exists(@"save.txt") && inputString == "f")
    {
      Console.Clear();
      DeleteSave();
    }
    else if(inputString == "s")
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
      Console.Clear();
      goto title;
    }
  }

  static void Instructions()
  {
    Console.WriteLine("Instructions \n\n\n");
    Console.WriteLine("In the KillAll game, your goal is to get as far as possible...\n");
    Console.ReadKey(true);
    Console.WriteLine("Before you die.");
    Console.ReadKey(true);
    Console.WriteLine("You will fight monsters.");
    Console.ReadKey(true);
    Console.WriteLine("Your blunt force attack hits the enemy hard, scaling off of your attack well. - 0 MP");
    Console.WriteLine("Your piercing attack ignores enemy defense. - 1 MP");
    Console.WriteLine("During fights, you may use mp to do massive damage to the monster - 4 MP");
    Console.WriteLine("You may also flee the fight. This ends the battle, but reduces progress.");
    Console.WriteLine("You may heal yourself in combat once per game.");
    Console.WriteLine("However, your health is restored after every battle.");
    Console.WriteLine("You may also instantly kill the enemy once per game.");
    Console.ReadKey(true);
    Console.Clear();

    TitleScreen();
  }
  static void Credits()
  {
    Console.WriteLine("EVERYTHING BY SHAUN LAZARO XG.");
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

  static string ChooseClass()
  {
    chooseName:
    Console.Clear();
    Console.WriteLine("Choose Your Class\n");
    Console.WriteLine("Quietist - Q\nWizard - W\nE.T. - E\nRogue - R");
    Console.WriteLine("Quietists are cool guys, but they don't lift.");
    Console.WriteLine("Wizards are smart, they have the magicks.");
    Console.WriteLine("Extra Terrestrials are strong, but not magical like the humans.");
    Console.WriteLine("Rogues are very very strong, but are vulnerable, and not magical.");
    
    input = Console.ReadKey(true);
    inputString = input.Key.ToString();
    inputString = inputString.ToLower();
    switch(inputString)
    {
      case"q":
      case"w":
      case"e":
      case"r":
      break;

      default:
      Console.Clear();
      Console.WriteLine("Press the correct key;")
      Console.ReadKey(true);
      goto chooseName;
    }
  
    return inputString;
  }

  static void LaunchGame(Character player)
  {
    Random rng = new Random();// Used to do any rng.
    Monster enemy = new Monster();
    //Game Loop.
    while(true)
    {
      if (player.progress % 10 == 0)
      {
        CreateMonster(player, 4, ref enemy);
      }
      else
      {
      CreateMonster(player, rng.Next( 1, 4 ), ref enemy);
      }
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
        WriteRed("You died.");
        player.PrintStats();
        Death(player);
      }
    }
  }

  // The CreateMonster method accepts a scenario from 1-3, then generates a monster with stats.
  // The method will also give a brief description to player before combat starts.
  static void CreateMonster(Character player, int scenario, ref Monster enemy)
  {
    long progress = player.progress;
    enemy.hp = progress * 15 + 20;
    enemy.atk = progress / 2 + 2;
    enemy.def = progress / 2 + 2;
    enemy.bossMonster = false;
    if(scenario == 1)
    {
      enemy.hp += progress * 0.5;
      enemy.def += progress * 0.5;
      Console.WriteLine("As you progress, the growls tell you that you are in claimed land...");
    }
    else if (scenario == 2)
    {
      enemy.atk += progress;
      Console.WriteLine("You hear the roar of an angry beast...");
    }
    else if (scenario == 3)
    {
      enemy.hp = enemy.hp / 2;
      enemy.atk = enemy.atk / 2;
      enemy.def = enemy.def / 2;
      Console.WriteLine("The creature in front of you cowers in fear, but won't let you progress.");
    }
    else if (scenario == 4)
    {
      enemy.hp = enemy.hp * 3;
      enemy.atk = enemy.atk * 2;
      enemy.def = enemy.def * 1.5;
      enemy.bossMonster = true;

      Random rng = new Random();
      int rngInt = rng.Next(1,4);
      if(rngInt == 1)
      {
        Console.WriteLine("You feel the air get heavy around you.");
        Console.ReadKey(true);
        Console.WriteLine("You turn.");
        Console.ReadKey(true);
        Console.WriteLine("You see a massive shadow.");
        Console.ReadKey(true);
        Console.WriteLine("In front of you, is the largest rat you've ever seen."); 
      }
      if(rngInt == 2)
      {
        Console.WriteLine("You feel a strong gust of wind.");
        Console.ReadKey(true);
        Console.WriteLine("You turn.");
        Console.ReadKey(true);
        Console.WriteLine("You see a massive shadow.");
        Console.ReadKey(true);
        Console.WriteLine("In front of you, is the largest bird you've ever seen."); 
      }
      if(rngInt == 3)
      {
        Console.WriteLine("You hear the voice of a another person.");
        Console.ReadKey(true);
        Console.WriteLine("No other human has come here in over a thousand years.");
        Console.ReadKey(true);
        Console.WriteLine("You turn.");
        Console.ReadKey(true);
        Console.WriteLine("In front of you, is the first human you've seen for a while now."); 
      }
    }
    enemy.PrintStats();
    Console.ReadKey(true);
    Console.Clear();
  }

  static bool Combat(ref Character player, Monster enemy)
  {
    double maxPlayerHp = player.hp;
    double maxPlayerMp =  player.mp;
    
    bool playerWin = false;

    while(player.hp > 1 && enemy.hp > 1)
    {

      combatstart:
      ConsoleKeyInfo keyPrompt = CombatPrompt(player, enemy);
      Console.Clear();
      if(keyPrompt.Key.ToString().ToLower() == "a")
      {
        Console.WriteLine("You hit the enemy hard, but the monster blocks some damage.");
        WriteDamage(player.CharacterAttack("strong", player, enemy));
        enemy.hp -= player.CharacterAttack("strong", player, enemy);
      }
      else if (keyPrompt.Key.ToString().ToLower() == "s")
      {
        if(player.mp > 0)
        {
          Console.WriteLine("You aim, and strike at the monster's weak point.");
          WriteDamage(player.CharacterAttack("pierce", player, enemy));
          enemy.hp -= player.CharacterAttack("pierce", player, enemy);
          player.mp--;
        }
        else
        {
          Console.WriteLine("You are too fatigued to do that");
          Console.ReadKey(true);
        }
      }
      else if(keyPrompt.Key.ToString().ToLower() == "h")
      {
        if(player.healed) 
        {
          Console.Clear();
          Console.WriteLine("You feel too fatigued to do that again.");
          Console.ReadKey(true);
          goto combatstart;
        }
        Console.WriteLine ("You focus, and your mind repairs itself.");
        Console.WriteLine ("You feel too fatigued to do that again.");
        player.hp = maxPlayerHp;
        player.healed = true;
      }
      else if(keyPrompt.Key.ToString().ToLower() == "d")
      {
        Console.WriteLine("You focus, and attempt to launch a powerful energy beam at the enemy.");
        if(player.mp > 3)
        {
          player.mp -= 4;
          Console.WriteLine("Your energy beam collides with the energy in a massive explosion.");
          player.CharacterAttack("destroy", player, enemy);
          enemy.hp -= player.CharacterAttack("destroy", player, enemy);
        }
        else
        {
          player.mp = 0;
          Console.WriteLine("You send some sparks from your hand, but you feel too drained to launch an energy beam.");
        }
      }
      else if (keyPrompt.Key.ToString().ToLower() == "f")
      {
        player.progress--;
        player.progress--;
        Console.WriteLine("You run back the way you came.");
        goto fleeing;
      }
      else if(keyPrompt.Key.ToString().ToLower() == "g")
      {
        if(player.freeKill)
        {
          enemy.hp = 0;
          Console.WriteLine("You pray to god, and the monster is condemned to hell.");
        }
        else
        {
          Console.WriteLine("You don't feel like God will help again.");
          goto combatstart;
        }
      }
      Console.ReadKey(true);

      if(enemy.hp > 0)
      {
      Console.WriteLine("The enemy returns with an attack of its own!");
      player.hp -= enemy.EnemyBasic(player, enemy);
      Console.ReadKey(true);
      }
    }
    
    if(player.hp >= 1)
    {
      playerWin = true;
      Console.WriteLine("The monster has been slain.");
      Console.ReadKey(true);
    }
    fleeing:
    if(player.hp >= 1)
    {
      playerWin = true;
      Console.ReadKey(true);
    }
    player.hp = maxPlayerHp;
    player.mp = maxPlayerMp;
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
    Console.WriteLine("\n Blunt Force Attack   - A" +
                      "\n Piercing Attack - 1MP- S" +
                      "\n Obliterate -4MP      - D" +
                      "\n Flee -1 Progress     - F" +
                      "\n Instant Kill         - G" +
                      "\n Heal Yourself        - H");
    playerPress = Console.ReadKey(true);
    string playerPressString = playerPress.Key.ToString();
    playerPressString = playerPressString.ToLower();
    switch(playerPressString)
    {
      case"a":
      case"s":
      case"d":
      case"f":
      case"g":
      case"h":
      break;

      default:
      Console.Clear();
      WriteRed("Please press the correct key.\n");
      Console.ReadKey(true);
      goto promptAction;      
    }
    return playerPress;
  }

  // Used For Errors.
  static void WriteRed(string s)
  {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(s);
    Console.ResetColor();
  }
  // Blue
  static void WriteDamage(double s)
  {
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("You did {0} damage", s);
    Console.ResetColor();
  }

  static void Death(Character player)
  {
    Console.ReadKey(true);
    Console.Clear();
    Console.WriteLine("Although it may seem pointless, every end is a new beginning.");
    Console.WriteLine("Your stats will increase based on how far you got.");
    Console.ReadKey();
    Console.Clear();
    player.DeathSave();
    while(true);
  }

  static void DeleteSave()
  {
    File.Delete(@"save.txt");
    Console.WriteLine("Deleted!");
    while(true);
  }
}
