using OOP.FighterAbstraction;
using System;
namespace OOP.Fighters
{
    public class Hero : Fighter
    {
        public static Ascii_Art FighterAsciiInfo { get; set; }

      
        public Hero(int hp) : base(hp, 0)//default 0 pos
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
