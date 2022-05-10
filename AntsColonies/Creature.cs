using System;

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

    public class Jerboa
    {
        internal int FirstDay;
        internal int LastDay;

        internal int RandomDay;
        
        internal Jerboa(int allDays)
        {
            FirstDay = Globals.Random.Next(1, allDays - 7); // день начала
            LastDay = FirstDay + 8;
            
            RandomDay = Globals.Random.Next(FirstDay, LastDay + 1); // когда нападет тушканчик
            
            //Console.WriteLine("First Day: " + FirstDay);
        }

        internal void Action()
        {
            foreach (Colony colony in Globals.Colonies)
            {
                colony.Workers.Clear();
            }
        }
    }
}