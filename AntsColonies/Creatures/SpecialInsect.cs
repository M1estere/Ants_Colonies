using System;

namespace AntsColonies
{
    internal class SpecialInsect : Creature
    {
        internal readonly SpecialInsectRank Rank;
        
        public SpecialInsect(SpecialInsectRank rank, Queen queen, int health, int protection, int damage) : base(queen, health, protection, damage)
        {
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
                default:
                    throw new ArgumentOutOfRangeException(nameof(rank), rank, null);
            }
        }

        internal void Collect(Squad squad, Stack stack)
        {
            if (Rank != SpecialInsectRank.Hardworker) return;
            
            if (stack.Dewdrops <= 0) return;
            
            stack.Dewdrops--;
            squad.Dewdrops++;
        }
    }
}