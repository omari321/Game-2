
using OOP.FighterAbstraction;
using OOP.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace OOP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var Game = new Game(10,3,1);
            Game.StartGame();
            // C-castle,H-hero,R-ranger,S-spearman,L-life
        }
        
        public class Game
        {
            private int _mapLenght;
            private int _maxEnemyNumber;
            private int _maxHeartNumber;
            private List<Fighter> _enemies;
            private List<int> _heartPos;
            private string _GameMap;
            private Hero _hero;

            public Game(int lenght,int enemyNumber,int heartNumber)
            {
                if (lenght < 10)
                {
                    _mapLenght = 10;
                }
                else
                {
                    _mapLenght = lenght;
                }
                _enemies=new List<Fighter>();
                _heartPos=new List<int> ();
                _maxEnemyNumber = enemyNumber;
                _maxHeartNumber = heartNumber;
                _hero=new Hero(100,0);
                _GameMap=DrawGameAndCharacters();

            }
            private string DrawGameAndCharacters()
            {
                StringBuilder map=new StringBuilder();
                int enemyNumber = 0;
                int heartNumber = 0;
                map.Append(_hero.Symbol);
                map.Append(" ");
                for (int i=2;i<_mapLenght-1;i++)
                {
                    if (enemyNumber<_maxEnemyNumber)
                    {
                        var Random=new Random();
                        var randomNumberi = Random.Next(100);

                        if (randomNumberi>=50 && enemyNumber<_maxEnemyNumber)
                        {
                            var enemy = this.GetPieces(Random.Next(100),i);
                            map.Append(enemy.Symbol);
                            _enemies.Add(enemy);
                            enemyNumber++;
                        }
                        else if (enemyNumber>0 && randomNumberi<=20 && _maxHeartNumber>heartNumber)
                        {
                            map.Append('L');
                            _heartPos.Add(i);
                            heartNumber++;
                        }
                        else
                        {
                            map.Append(' ');
                        }
                        
                    }
                    else
                    {
                        map.Append(" ");
                    }
                }
                map.Append("C");
                map.Append("#\n");
                map.Append(new String('#',_mapLenght+1));
                map.Append($"\nHero Health :{_hero.GetHp()}");
                map.Append($"\nmost Left Enemy Hp : {_enemies[0].GetHp()} ");
                return map.ToString(); 
                
            }
            private string ReRender()
            {
                StringBuilder map = new StringBuilder();
                map.Append(new String(' ',_hero.Position));
                map.Append('H');
                var curPos = _hero.Position;
                if (_enemies.Count>0)
                {
                    foreach (var enemy in _enemies)
                    {
                        map.Append(new String(' ',enemy.Position-curPos-1));
                        map.Append(enemy.Symbol);
                        curPos=enemy.Position;
                    }
                }
                map.Append(new String(' ',_mapLenght-1-curPos-1));
                foreach(var heart in _heartPos)
                {
                    map[heart] = 'L';
                }
                map.Append("C");
                map.Append("#\n");
                map.Append(new String('#', _mapLenght + 1));
                map.Append($"\nHero Health :{_hero.GetHp()}");
                if (_enemies.Count()>0)
                {
                    map.Append($"\nmost Left Enemy Hp : {_enemies[0].GetHp()} ");
                }
                return map.ToString();
            }
            private Fighter GetPieces(int number,int position)
            {
                if (number<50)
                {
                    return new Ranger(100,position);
                }
                else
                {
                    return new SpearMan(100,position);
                }
            }
            public void StartGame()
            {
                while(_hero.Position!=_mapLenght-2 && _hero.GetHp()>0)
                {
                    Console.Clear();
                    Console.WriteLine(_GameMap);
                    if(_enemies.Count() > 0 && _enemies[0].GetHp()<1)
                        {
                            _enemies.RemoveAt(0);
                        }
                    if (_enemies.Count() > 0)
                    {
                        

                        var rangeBetween = Math.Abs(_hero.Position - _enemies[0].Position);
                        if (_hero.GetRange() >= rangeBetween)
                        {
                            _enemies[0].TakeDamage(_hero.GetRandomDamage());
                        }
                        else
                        {
                            _hero.Position += 1;
                            if (_GameMap[_hero.Position]=='L')
                            {
                                _hero.TakeDamage(-200 + _hero.GetHp());
                                _heartPos.RemoveAt(0);
                            }
                        }
                        if (_enemies[0].GetRange() >= rangeBetween)
                        {
                            _hero.TakeDamage(_enemies[0].GetRandomDamage());
                        }
                    }
                    else
                    {
                        _hero.Position += 1;
                    }
                    if (_hero.Position<=_mapLenght-1)
                    {   
                        _GameMap = ReRender();
                    }
                    Thread.Sleep(1000);
                }
                if (_hero.GetHp()>0)
                {
                    Console.WriteLine("gilocavt tqven moiget");
                }
                else
                {
                    Console.WriteLine("tqven caaget");
                }
            }
        }
    }
}
