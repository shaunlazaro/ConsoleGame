using System;

class Character
{
  public int hp;
  public int def;
  public int atk;
  public int progress;
  public string name;

  public void PrintStats()
  {
    Console.WriteLine("\n{0}'s HP: {1}\n{0}'s ATK: {2}\n {0}'s DEF: {3}\n",
                      name, hp, atk, def);
    Console.WriteLine("Progress Made: {0}\n",progress);
  }
}
