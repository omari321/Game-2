using OOP.FighterAbstraction;
using OOP.Fighters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OOP.GameEngine
{
    public class Game
    {
        private int _mapLenght;
        private int _maxEnemyNumber;
        private int _maxHeartNumber;
        private List<Fighter> _enemies;
        private List<int> _heartPos;
        private string _GameMap;
        private Hero _hero;
        private int _HeroHP = 200;
        private Ascii_Art _HeartAscii;
        private Ascii_Art _CastleAscii;
        private List<Ascii_Art> _GameWinAscii;
        private Ascii_Art _GameloseAscii;
        private const char _HeroSymbol = 'H';
        private const char _SpearManSymbol = 'S';
        private const char _RangerSymbol = 'R';
        private const char _CastleSymbol = 'C';
        private const char _HealthSymbol = 'L';

        public Game(int lenght, int enemyNumber, int heartNumber, string heroFIle, string SpearManFile, string rangerFile, string heartFile, string castleFIle,List<string> winFiles,string loseAscii)
        {
            if (lenght < 10)
            {
                _mapLenght = 10;
            }
            else
            {
                _mapLenght = lenght;
            } 
            _GameloseAscii = new Ascii_Art(loseAscii);
            Ascii_Art.maxheight = 0;
            _enemies = new List<Fighter>();
            _GameWinAscii=new List<Ascii_Art>();
            _heartPos = new List<int>();
            _maxEnemyNumber = enemyNumber;
            _maxHeartNumber = heartNumber;
            SpearMan.SetFighterAscii(SpearManFile, _SpearManSymbol.ToString()); 
            Hero.SetFighterAscii(heroFIle, _HeroSymbol.ToString());
            Ranger.SetFighterAscii(rangerFile, _RangerSymbol.ToString());
            _HeartAscii=new Ascii_Art(heartFile,Game._HealthSymbol.ToString());
            _CastleAscii = new Ascii_Art(castleFIle,Game._CastleSymbol.ToString());
            winFiles.ForEach(str => _GameWinAscii.Add(new Ascii_Art(str)));
           
            _hero = new Hero(_HeroHP);
            _GameMap = DrawGameAndCharacters();
            

        }
        private string DrawGameAndCharacters()
        {
            StringBuilder map = new StringBuilder();
            int enemyNumber = 0;
            int heartNumber = 0;
            map.Append(Hero.FighterAsciiInfo.symbol);
            map.Append(" ");
            for (int i = 2; i < _mapLenght - 1; i++)
            {
                if (enemyNumber < _maxEnemyNumber)
                {
                    var Random = new Random();
                    var randomNumberi = Random.Next(100);

                    if (randomNumberi >= 50 && enemyNumber < _maxEnemyNumber)
                    {
                        var enemy = this.GetPieces(Random.Next(100), i);
                        map.Append(enemy.getFighterAscii().symbol);
                        _enemies.Add(enemy);
                        enemyNumber++;
                    }
                    else if (enemyNumber > 0 && randomNumberi <= 20 && _maxHeartNumber > heartNumber)
                    {
                        map.Append(_HealthSymbol);
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
            return map.ToString();

        }
        private Fighter GetPieces(int number, int position)
                {
                    if (number < 50)
                    {
                        return new Ranger(100, position);
                    }
                    else
                    {
                        return new SpearMan(100, position);
                    }
                }
        private string ReRender()
        {
            StringBuilder map = new StringBuilder();

            map.Append(new String(' ', _hero.Position));
            map.Append(_HeroSymbol);

            var curPos = _hero.Position;
            //populate with enemies
            foreach (var enemy in _enemies)
            {
                map.Append(new String(' ', enemy.Position - curPos - 1));
                map.Append(enemy.getFighterAscii().symbol);
                curPos = enemy.Position;
            }
            map.Append(new String(' ', _mapLenght - 1 - curPos - 1));
            
            //change empty positions with life
            foreach (var heart in _heartPos)
            {
                map[heart] =Convert.ToChar(_HealthSymbol);
            }

            map.Append("C");         
            return map.ToString();
        }
        private string AsciiRender(string map)
        {
            var newMap=new StringBuilder();
            var enemy = 0;
            Console.WriteLine(map);
            for (var i=0;i<Ascii_Art.maxheight+1;i++)
            {
                foreach(var symbol in map)
                {
                    string AsciiLine = default(string);
                    var index = 0;
                    switch (symbol)
                    {
                        case   ' ':
                            newMap.Append(new String(' ', 15));
                            break;
                        case _HeroSymbol:
                            index = Ascii_Art.maxheight+1 - Hero.FighterAsciiInfo.lines.Count;
                            if (i == 0)
                            {
                                var str = $"Hero HP {_hero.GetHp()}";
                                newMap.Append(str);
                                newMap.Append(new String(' ',Hero.FighterAsciiInfo.width-str.Length));
                            }
                            else if (index <= i)
                            {
                                if( Hero.FighterAsciiInfo.lines.Count > i - index)
                                {
                                    AsciiLine = Hero.FighterAsciiInfo.lines[i - index];
                                    newMap.Append(AsciiLine);
                                    newMap.Append(new String(' ', Hero.FighterAsciiInfo.width - AsciiLine.Length));
                                }
                                else
                                {
                                    newMap.Append(new String(' ', Hero.FighterAsciiInfo.width));
                                }

                            }
                            else
                            {
                                newMap.Append(new String(' ', Hero.FighterAsciiInfo.width));
                            }
                            break;
                        case _SpearManSymbol:
                            index = Ascii_Art.maxheight+1 - SpearMan.FighterAsciiInfo.lines.Count;
                            if (i == 0)
                            {

                                string str="";
                                if (_enemies.Count>0)
                                    str= $"HP {_enemies[enemy].GetHp()}";
                                newMap.Append(str);
                                newMap.Append(new String(' ', _enemies[enemy].getFighterAscii().width - str.Length));
                                enemy += 1;
                            }
                            else if (index<=i)
                            {
                                if (SpearMan.FighterAsciiInfo.lines.Count > i-index)
                                {

                                    AsciiLine = SpearMan.FighterAsciiInfo.lines[i-index];
                                    newMap.Append(AsciiLine);
                                    newMap.Append(new String(' ', SpearMan.FighterAsciiInfo.width - AsciiLine.Length));
                                }
                                else
                                {
                                    newMap.Append(new String(' ', SpearMan.FighterAsciiInfo.width));
                                }
                                
                            }
                            else
                            {
                                newMap.Append(new String(' ', SpearMan.FighterAsciiInfo.width));
                            }
                            break;
                        case _RangerSymbol:
                            index = Ascii_Art.maxheight+1 - Ranger.FighterAsciiInfo.lines.Count;
                            if (i == 0)
                            {
                                string str = "";
                                if (_enemies.Count > 0)
                                    str = $"HP {_enemies[enemy].GetHp()}";
                                newMap.Append(str);
                                newMap.Append(new String(' ', _enemies[enemy].getFighterAscii().width - str.Length));
                                enemy += 1;
                            }
                            else if (index <= i)
                            {
                                if (SpearMan.FighterAsciiInfo.lines.Count > i - index)
                                {

                                    AsciiLine = Ranger.FighterAsciiInfo.lines[i - index];
                                    newMap.Append(AsciiLine);
                                    newMap.Append(new String(' ', Ranger.FighterAsciiInfo.width - AsciiLine.Length));
                                }
                                else
                                {
                                    newMap.Append(new String(' ', Ranger.FighterAsciiInfo.width));
                                }

                            }
                            else
                            {
                                newMap.Append(new String(' ', Ranger.FighterAsciiInfo.width));
                            }
                            break;
                        case _HealthSymbol:
                            index = Ascii_Art.maxheight + 1 - _HeartAscii.lines.Count;
                            if (i == 0)
                            {
                                newMap.Append(new String(' ',_HeartAscii.width));
                            }
                            else if (index <= i)
                            {
                                if (_HeartAscii.lines.Count > i - index)
                                {

                                    AsciiLine = _HeartAscii.lines[i - index];
                                    newMap.Append(AsciiLine);
                                    newMap.Append(new String(' ', _HeartAscii.width - AsciiLine.Length));
                                }
                                else
                                {
                                    newMap.Append(new String(' ', _HeartAscii.width));
                                }

                            }
                            else
                            {
                                newMap.Append(new String(' ', _HeartAscii.width));
                            }
                            break;
                        case _CastleSymbol:
                            index = Ascii_Art.maxheight + 1 - _CastleAscii.lines.Count;
                            if (i == 0)
                            {
                                newMap.Append(new String(' ', _CastleAscii.width));
                            }
                            else if (index <= i)
                            {
                                if (_CastleAscii.lines.Count > i - index)
                                {

                                    AsciiLine = _CastleAscii.lines[i - index];
                                    newMap.Append(AsciiLine);
                                    newMap.Append(new String(' ', _CastleAscii.width - AsciiLine.Length));
                                }
                                else
                                {
                                    newMap.Append(new String(' ', _CastleAscii.width));
                                }

                            }
                            else
                            {
                                newMap.Append(new String(' ', _CastleAscii.width));
                            }
                            break;
                    }
                    
                }newMap.Append('\n');
            }
            Console.WriteLine(newMap.ToString());
            return newMap.ToString();
        }
       
        public void StartGame()
        {
            AsciiRender(_GameMap);
            Thread.Sleep(1000);
            Console.Clear();
            while (_hero.Position != _mapLenght - 2 && _hero.GetHp() > 0)
            {
                Console.Clear();
                AsciiRender(_GameMap);
                if (_enemies.Count() > 0 && _enemies[0].GetHp() < 1)
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
                        if (_GameMap[_hero.Position] ==Convert.ToChar(_HealthSymbol))
                        {
                            _hero.TakeDamage(-_HeroHP + _hero.GetHp());
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
                if (_hero.Position <= _mapLenght - 1)
                {
                    _GameMap =ReRender();
                }
                Thread.Sleep(1000);
            }


            Console.Clear();
            AsciiRender(_GameMap);
            Thread.Sleep(1000);
            Console.Clear();
            if (_hero.GetHp() > 0)
            {
                Console.WriteLine("aloo");
                _GameWinAscii.ForEach(ascii =>
                {
                    Console.WriteLine("gilocavt tqven moiget");
                    foreach(var line in ascii.lines)
                    {
                        Console.WriteLine(line);
                    }
                    Thread.Sleep(1000);
                    Console.Clear();
                });
                
            }
            else
            {
                Console.WriteLine("tqven caaget");
                foreach (var line in _GameloseAscii.lines)
                {
                    Console.WriteLine(line);
                }
                Thread.Sleep(10000);
            }
        }
    }

}
