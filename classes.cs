using System;

abstract class Entity
{
  public double hp;
  public double def;
  public double atk;
  public int progress;
  public string name;

}

class Character:Entity
{

  public void PrintStats()
  {
    Console.WriteLine("\n{0}'s HP: {1}\n{0}'s ATK: {2}\n{0}'s DEF: {3}\n",
                      name, hp, atk, def);
    Console.WriteLine("{0}'s Progress: {1}\n", name, progress);
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
    else
    {
      result = (player.atk * 0.6);
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
    Console.WriteLine("{0}'s Progress: {1}\n", "Monster", progress);
  }
  public double EnemyBasic(Character player, Monster enemy)
  {
    double result = enemy.atk - player.def;
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