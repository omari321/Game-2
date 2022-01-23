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
        public static Ascii_Art FighterAsciiInfo { get; set; }

        
        public Ranger(int hp, int position) : base(hp, position)
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
        public static void SetFighterAscii(string fileLoc, string symbol)
        {
            System.Console.WriteLine(symbol);
            FighterAsciiInfo = new Ascii_Art(fileLoc, symbol);
        }

        public override Ascii_Art getFighterAscii()
        {
            return FighterAsciiInfo;
        }
    }

}
