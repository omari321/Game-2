using OOP.FighterAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Fighters
{
    public class Ranger :Fighter
    {
        public const string Symbol = "R";
        public Ranger(int hp, int position) : base(hp, "R", position)
        {

        }

        public override int GetRandomDamage()
        {
            return new Random().Next(20);
        }

        public override int GetRange()
        {
            return new Random().Next(1, 4);
        }
    }

}
