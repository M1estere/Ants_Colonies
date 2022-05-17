using System.Collections.Generic;

namespace AntsColonies
{
    public class Queen : Creature
    {
        internal readonly string Name;

        internal Colony Colony;
        
        // growth cycle
        private readonly int _minGrowthCycle;
        private readonly int _maxGrowthCycle;

        // larvas amount
        private readonly int _minLarvas;
        private readonly int _maxLarvas;

        private int _maxQueensToMake;

        private readonly bool _canMakeQueens;

        internal readonly List<Queen> FriendQueens = new List<Queen>();
        
        public Queen(string name, int health, int protection, int damage, int minGrowthCycle, int maxGrowthCycle, int minLarvas, int maxLarvas, bool canMakeQueens, Colony? colony = null) : base(health, protection, damage)
        {
            Colony = colony;
            
            Name = name;
            
            _minGrowthCycle = minGrowthCycle;
            _maxGrowthCycle = maxGrowthCycle;

            _minLarvas = minLarvas;
            _maxLarvas = maxLarvas;

            _canMakeQueens = canMakeQueens;
            
            _maxQueensToMake = _maxLarvas;
        }

        internal void MakeLarvas()
        {
            int larvasAmount = Globals.Random.Next(_minLarvas, _maxLarvas + 1);

            for (int i = 0; i < larvasAmount; i++) // spawn larvas
            {
                int larvaType = Globals.Random.Next(0, 100 + 1);
                switch (larvaType)
                {
                    // worker
                    case <= 65:
                    {
                        int growthCycle = Globals.Random.Next(_minGrowthCycle, _maxGrowthCycle + 1);

                        Larva larva = new Larva(growthCycle, Larva.LarvaType.Worker, this);
                        Colony.Larvas.Add(larva);
                        break;
                    }
                    // warrior
                    case > 65 and <= 90:
                    {
                        int growthCycle = Globals.Random.Next(_minGrowthCycle, _maxGrowthCycle + 1);
                    
                        Larva larva = new Larva(growthCycle, Larva.LarvaType.Warrior, this);
                        Colony.Larvas.Add(larva);
                        break;
                    }
                    // queen
                    case > 90 and <= 100 when _canMakeQueens && _maxQueensToMake > 0:
                    {
                        _maxQueensToMake--;
                    
                        int growthCycle = Globals.Random.Next(_minGrowthCycle, _maxGrowthCycle + 1);
                    
                        Larva larva = new Larva(growthCycle, Larva.LarvaType.Queen, this);
                        Colony.Larvas.Add(larva);
                        break;
                    }
                }
            }
        }
    }
}