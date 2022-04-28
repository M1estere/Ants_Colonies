namespace AntsColonies
{
    public class Creature
    {
        internal int Health;
        internal int Protection;
        internal int Damage;

        public Creature(int health, int protection, int damage/*, Resource[]? _canTake = null*/)
        {
            Health = health;
            Protection = protection;
            Damage = damage;

            Health = Health + Protection;
        }

        protected Creature()
        {
            
        }

        internal virtual void GetInfo() { }
    }
}