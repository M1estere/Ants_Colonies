using System;

namespace AntsColonies
{
    public class AntWarrior : Creature
    {
        internal string _queenName;
        internal CreatureRank Rank;

        internal bool CanAttack = true;
        
        private int firstBlood;
        
        public AntWarrior(int _health, int _protection, int _damage, string queenName, CreatureRank _rank) : base(_health, _protection, _damage)
        {
            _queenName = queenName;
            Rank = _rank;
            switch (_rank)
            {
                case CreatureRank.Common:
                    Health = 1;
                    Protection = 0;
                    Damage = 1;
                    break;
                case CreatureRank.Advanced:
                    Health = 6;
                    Protection = 2;
                    Damage = 4;
                    break;
                case CreatureRank.Elder:
                    Health = 2;
                    Protection = 1;
                    Damage = 2;
                    break;
                case CreatureRank.Elite:
                    Health = 8;
                    Protection = 4;
                    Damage = 3;
                    break;
                case CreatureRank.Legend:
                    Health = 8;
                    Protection = 6;
                    Damage = 4;
                    break;
                default:
                    break;
            }

            Health = Protection + Health;
        }
        
        internal override void GetInfo()
        {
            Console.WriteLine("\n---------------------------------------------\n");
            Console.WriteLine($"Муравей - Воин ({Rank})\nЗдоровье: {Health}; Защита: {Protection}; Урон: {Damage}");
            Console.WriteLine("\n---------------------------------------------\n");
        }

        internal void GetQueenInfo() => Console.WriteLine($"Моя королева: {_queenName}");

        internal void Attack(Creature enemy)
        {
            firstBlood = Globals.Random.Next(0, 2);
            
            while (enemy.Health > 0)
            {
                if (enemy is AntWorker)
                {
                    enemy.Health -= Damage;
                } else if ((enemy is AntWarrior) || (enemy is SpecialInsect))
                {
                    StartBattle(enemy);
                }

                if (Health <= 0)
                    break;
            }
        }

        private void StartBattle(Creature enemy)
        {
            if (firstBlood % 2 == 0)
            {
                Health -= enemy.Damage;
                enemy.Health -= Damage;
            } else
            {
                enemy.Health -= Damage;
                Health -= enemy.Damage;
            }
        }
    }
}