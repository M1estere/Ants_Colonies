namespace AntsColonies
{
    internal class SpecialInsect : Creature
    {
        public SpecialInsect(int _health, int _protection, int _damage, Resource[]? _canTake = null) : base(_health, _protection, _damage/*, _canTake*/)
        {
            
        }
    }
}