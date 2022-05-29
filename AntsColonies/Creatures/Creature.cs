namespace AntsColonies
{
    public class Creature
    {
        internal Queen Queen;
        
        internal int Health;
        internal int Protection;
        internal int Damage;

        protected Creature(int health, int protection, int damage)
        {
            Health = health;
            Protection = protection;
            Damage = damage;

            Health += Protection;
        }
        
        protected Creature(Queen queen, int health, int protection, int damage)
        {
            Queen = queen;
            
            Health = health;
            Protection = protection;
            Damage = damage;

            Health += Protection;
        }
    }
}