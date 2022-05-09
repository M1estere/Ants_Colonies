using System;

namespace AntsColonies
{
    public class AntWarrior : Creature
    {
        internal WarriorRank Rank;

        // attack params
        internal int AntsToAttack;

        private int _maxBiteCount;
        private int _currentBiteCount = 0;
        
        public AntWarrior(int _health, int _protection, int _damage, Queen _queen, WarriorRank _rank) : base(_queen, _health, _protection, _damage)
        {
            Queen = _queen;
            Rank = _rank;
            
            switch (_rank)
            {
                case WarriorRank.Common:
                    Health = 1;
                    Protection = 0;
                    Damage = 1;
                    AntsToAttack = 1;
                    _maxBiteCount = 1;
                    break;
                case WarriorRank.Advanced:
                    Health = 6;
                    Protection = 2;
                    Damage = 4;
                    AntsToAttack = 2;
                    _maxBiteCount = 1;
                    break;
                case WarriorRank.Elder:
                    Health = 2;
                    Protection = 1;
                    Damage = 2;
                    AntsToAttack = 1;
                    _maxBiteCount = 1;
                    break;
                case WarriorRank.Elite:
                    Health = 8;
                    Protection = 4;
                    Damage = 3;
                    AntsToAttack = 2;
                    _maxBiteCount = 2;
                    break;
                case WarriorRank.Legend:
                    Health = 8;
                    Protection = 6;
                    Damage = 4;
                    AntsToAttack = 3;
                    _maxBiteCount = 1;
                    break;
                default:
                    break;
            }

            Health += Protection;
        }
        
        internal override void GetInfo()
        {
            Console.WriteLine("\n---------------------------------------------\n");
            Console.WriteLine($"Муравей - Воин ({Rank})\nЗдоровье: {Health}; Защита: {Protection}; Урон: {Damage}");
            Console.WriteLine("\n---------------------------------------------\n");
        }

        internal void GetQueenInfo() => Console.WriteLine($"Моя королева: {Queen.Name}");

        internal void Attack(Creature enemy)
        {
            if (enemy == null) return;
            while (_currentBiteCount < _maxBiteCount)
            {
                _currentBiteCount++;
                
                if (enemy is AntWorker)
                {
                    enemy.Health -= enemy.Health; // сразу убивает рабочего
                } else if ((enemy is AntWarrior) || (enemy is SpecialInsect))
                {
                    Fight(enemy); // наносит урон
                }
            }
            _currentBiteCount = 0;
        }

        private void Fight(Creature enemy)
        {
            if (Health > 0 && enemy.Health > 0)
            {
                int randomPunch = Globals.Random.Next(0, 2);

                if (randomPunch % 2 == 0)
                {
                    if (Health > 0)
                        enemy.Health -= Damage;
                    
                    if (enemy.Health > 0)
                        Health -= enemy.Damage;
                } else
                {
                    if (enemy.Health > 0)
                        Health -= enemy.Damage;
                    
                    if (Health > 0)
                        enemy.Health -= Damage;
                }
            }
        }
    }
}