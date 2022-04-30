namespace AntsColonies
{
    internal class Queen : Creature
    {
        internal string Name;

        private int _minGrowthCycle;
        private int _maxGrowthCycle;
        
        public Queen(string _name, int _health, int _protection, int _damage, int minGrowthCycle, int maxGrowthCycle, Resource[]? _canTake = null) : base(_health, _protection, _damage/*, _canTake*/)
        {
            Name = _name;
            
            _minGrowthCycle = minGrowthCycle;
            _maxGrowthCycle = maxGrowthCycle;
        }
    }
}