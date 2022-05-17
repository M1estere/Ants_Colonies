namespace AntsColonies
{
    public class Creature
    {
        internal Queen Queen;
        
        internal int Health;
        internal int Protection;
        internal int Damage;

        protected Creature(int health, int protection, int damage/*, Resource[]? _canTake = null*/)
        {
            Health = health;
            Protection = protection;
            Damage = damage;

            Health += Protection;
        }
        
        protected Creature(Queen queen, int health, int protection, int damage/*, Resource[]? _canTake = null*/)
        {
            Queen = queen;
            
            Health = health;
            Protection = protection;
            Damage = damage;

            Health += Protection;
        }

        protected Creature()
        {
            
        }

        internal virtual void GetInfo() { }
    }
}