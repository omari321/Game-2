using OOP.FighterAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP.Fighters
{
    public class SpearMan : Fighter
    {
        public const String symbol = "S";
        public SpearMan(int hp, int position) : base(hp, "S", position)
        {

        }

        public override int GetRandomDamage()
        {
            return new Random().Next(20);
        }

        public override int GetRange()
        {
            return 1;
        }
    }

}
