using OOP.Fighters;
using System.Collections.Generic;

namespace OOP.FighterAbstraction
{
    public abstract class Fighter
    {
        private int _hp;
        private int _range;
        public int Position { get; set; }
        public Fighter(int hp, int position)
        {
            _hp = hp;
            Position = position;
            _range = GetRange();
        }


        public abstract int GetRandomDamage();
        //virtual sacdelad
        public virtual int GetRange()
        {
            return 0;
        }
        public virtual void TakeDamage(int damage)
        {
            _hp -= damage;
        }
        public int GetHp()
        {
            return _hp;
        }
        abstract public Ascii_Art getFighterAscii();
    }
}
