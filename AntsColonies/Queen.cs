namespace AntsColonies
{
    internal class Queen : Creature
    {
        internal string Name { get; set; }
        
        public Queen(string _name, int _health, int _protection, int _damage, Resource[]? _canTake = null) : base(_health, _protection, _damage/*, _canTake*/)
        {
            Name = _name;
        }
    }
}