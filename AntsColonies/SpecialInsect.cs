namespace AntsColonies
{
    internal class SpecialInsect : Creature
    {
        internal SpecialInsectRank Rank;

        internal string Name;
        
        public SpecialInsect(string name, SpecialInsectRank rank, Queen queen, int _health, int _protection, int _damage) : base(queen, _health, _protection, _damage/*, _canTake*/)
        {
            Name = name;
            Rank = rank;

            Damage = 0;
            
            switch (rank)
            {
                case SpecialInsectRank.Hardworker:
                    Health = 17;
                    Protection = 6;
                    break;
                case SpecialInsectRank.Lazy:
                    Health = 27;
                    Protection = 5;
                    break;
            }
        }

        internal void Collect(Squad squad, Stack stack)
        {
            if (Rank == SpecialInsectRank.Hardworker)
            {
                if (stack.Dewdrops > 0)
                {
                    stack.Dewdrops--;
                    squad.Dewdrops++;
                }
            }
        }
    }
}