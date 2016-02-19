using System;

abstract class Entity
{
  public double hp;
  public double def;
  public double atk;
  public int progress;
  public string name;

  public void PrintStats()
  {
    Console.WriteLine("\n{0}'s HP: {1}\n{0}'s ATK: {2}\n{0}'s DEF: {3}\n",
                      name, hp, atk, def);
    Console.WriteLine("{0}'s Progress: {0}\n",progress);
  }

}

class Character:Entity
{

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
      result = (player.atk * 0.5);
    }

    return result;
  }
}

class Monster:Entity
{
  public double EnemyBasic(Character player, Monster enemy)
  {
    double result = enemy.atk - player.def;
    return result;
  }
  public double EnemyStrong(ref Character player, ref Monster enemy)
  {
    double result = (enemy.atk * 0.7);
    return result; 
  }
}