using System.Collections.Generic;

namespace AntsColonies
{
    public class Queen : Creature
    {
        internal string Name;

        internal Colony Colony;
        
        // growth cycle
        private int _minGrowthCycle;
        private int _maxGrowthCycle;

        // larvas amount
        private int _minLarvas;
        private int _maxLarvas;

        private int _maxQueensToMake;

        private bool _canMakeQueens;

        internal List<Queen> FriendQueens = new List<Queen>();
        
        public Queen(string _name, int _health, int _protection, int _damage, int minGrowthCycle, int maxGrowthCycle, int minLarvas, int maxLarvas, bool canMakeQueens, Colony? colony = null) : base(_health, _protection, _damage)
        {
            Colony = colony;
            
            Name = _name;
            
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
                if (larvaType <= 65) // worker
                {
                    int growthCycle = Globals.Random.Next(_minGrowthCycle, _maxGrowthCycle + 1);

                    Larva larva = new Larva(growthCycle, Larva.LarvaType.Worker, this);
                    Colony.Larvas.Add(larva);
                } else if (larvaType > 65 && larvaType <= 90) // warrior
                {
                    int growthCycle = Globals.Random.Next(_minGrowthCycle, _maxGrowthCycle + 1);
                    
                    Larva larva = new Larva(growthCycle, Larva.LarvaType.Warrior, this);
                    Colony.Larvas.Add(larva);
                } else if ((larvaType > 90 && larvaType <= 100) && _canMakeQueens && _maxQueensToMake > 0) // queen
                {
                    _maxQueensToMake--;
                    
                    int growthCycle = Globals.Random.Next(_minGrowthCycle, _maxGrowthCycle + 1);
                    
                    Larva larva = new Larva(growthCycle, Larva.LarvaType.Queen, this);
                    Colony.Larvas.Add(larva);
                }
            }
        }
    }
}