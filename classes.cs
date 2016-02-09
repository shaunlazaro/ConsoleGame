using System;

abstract class Entity
{
  public int hp;
  public int def;
  public int atk;
  public int progress;
  public string name;

}

class Character:Entity
{
  public void PrintStats()
  {
    Console.WriteLine("\n{0}'s HP: {1}\n{0}'s ATK: {2}\n{0}'s DEF: {3}\n",
                      name, hp, atk, def);
    Console.WriteLine("Progress Made: {0}\n",progress);
  }
  public void CharacterAttack(string attack, out Character player, out Monster enemy)
  {
    // Strong attack
    if(attack == "strong")
    {
      enemy.HP =- (player.atk * 1.25) - enemy.def;
    }
    // Hits defense-focussed enemies harder.
    else if (attack == "other")
    {
      enemy.HP =- (player.atk * 0.5);
    }
  }
}

class Monster:Entity
{

}