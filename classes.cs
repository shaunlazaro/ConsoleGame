using System;
using System.IO;

abstract class Entity
{
  public double hp;
  public double mp;
  public double def;
  public double atk;
  public int progress;
  public int initialProgress;
  public string name;

}

class Character:Entity
{
  public bool healed;

  public void PrintStats()
  {
    Console.WriteLine("\n{0}'s HP: {1}\n{0}'s MP: {4}\n{0}'s ATK: {2}\n{0}'s DEF: {3}\n",
                      name, hp, atk, def, mp);
    Console.WriteLine("{0}'s Progress: {1}\n", name, progress);
  }

  public void DeathSave()
  {
    Console.WriteLine("Your progress will now be printed to the file, if you got further than last time!");
    if (progress > initialProgress)
    {
      using(StreamWriter saveStats = new StreamWriter("save.txt"))
      {
        saveStats.WriteLine(progress);
        saveStats.WriteLine(name);
      }
      Console.ReadKey(true);
    }
    else
    {
      Console.WriteLine("Well, you didn't get further, suks 4 u");
    }
  }

  public double CharacterAttack(string attack, Character player, Monster enemy)
  {
    double result;
    // Strong attack
    if(attack == "strong")
    {
      result = (player.atk) - enemy.def;
    }
    // Hits defense-focussed enemies harder.
    else if (attack == "pierce")
    {
      result = player.atk;
    }
    else if (attack == "destroy")
    {
      result = player.atk * 4 - enemy.def;
    }
    else
    {
      result = 0;
      Console.WriteLine("Game broke");
      while (true);
    }
    return result;
  }
}

class Monster:Entity
{

  public void PrintStats()
  {
    Console.WriteLine("\n{0}'s HP: {1}\n{0}'s ATK: {2}\n{0}'s DEF: {3}\n",
                      "Monster", hp, atk, def);
  }
  public double EnemyBasic(Character player, Monster enemy)
  {
    double result = enemy.atk - player.def;
    if(result < 0){result = 1;}
    Console.WriteLine("The monster strikes you for {0} damage", result);
    return result;
  }
  public double EnemyStrong(Character player, Monster enemy)
  {
    double result = (enemy.atk * 1.5 - player.def);
    Console.WriteLine("The monster strikes you for {0} damage", result);
    return result; 
  }
}