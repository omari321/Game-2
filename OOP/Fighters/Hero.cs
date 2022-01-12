using OOP.FighterAbstraction;
using System;
namespace OOP.Fighters
{
    public class Hero : Fighter
    {
        public Hero(int hp, int position) : base(hp, "H", position)
        {
        }

        public override int GetRandomDamage()
        {
            return new Random().Next(20, 50);
        }

        public override int GetRange()
        {
            return 1;
        }
    }

}
