
using OOP.FighterAbstraction;
using OOP.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OOP.GameEngine;
using System.IO;

namespace OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> winFiles=new List<string>() {"win1.txt", "win2.txt", "win3.txt" };
            var Game = new Game(12,4,1,"Hero_1.txt","spear_man.txt","ranger.txt","life.txt","castle.txt",winFiles,"lose.txt");
            Game.StartGame();
            
        }    
    }
}
