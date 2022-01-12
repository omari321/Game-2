namespace OOP.FighterAbstraction
{
    public abstract class Fighter
    {
        public string Symbol { get; }
        private int _hp;
        private int _range;
        public int Position { get; set; }

        public Fighter(int hp, string symbol, int position)
        {
            Symbol = symbol;
            _hp = hp;
            Position = position;
            _range = GetRange();
        }
        public abstract int GetRandomDamage();
        public abstract int GetRange();
        public virtual void TakeDamage(int damage)
        {
            _hp -= damage;
        }
        public int GetHp()
        {
            return _hp;
        }
    }
}
