using System;
using System.Dynamic;

namespace AntsColonies
{
    public class AntWorker : Creature
    {
        private string _queenName;
        
        internal CreatureRank Rank;

        private Resource[] _canTake;
        
        public AntWorker(int _health, int _protection, int _damage, string queenName, CreatureRank _rank) : base(_health, _protection, _damage)
        {
            _queenName = queenName;
            Rank = _rank;
            switch (_rank)
            {
                case CreatureRank.Common:
                    Health = 1;
                    Protection = 0;
                    break;
                case CreatureRank.Elder:
                    Health = 2;
                    Protection = 1;
                    break;
                case CreatureRank.Elite:
                    Health = 8;
                    Protection = 4;
                    break;
                case CreatureRank.Legend:
                    Health = 10;
                    Protection = 6;
                    break;
                case CreatureRank.Advanced:
                    Health = 6;
                    Protection = 6;
                    break;
                default:
                    break;
            }

            Health = Health + Protection;
        }

        private void Collect()
        {
            
        }

        internal override void GetInfo()
        {
            Console.WriteLine("\n---------------------------------------------\n");
            Console.WriteLine($"Муравей - Рабочий ({Rank})\nЗдоровье: {Health}; Защита: {Protection}; Урон: {Damage}");
            Console.WriteLine("\n---------------------------------------------\n");
        }
        
        internal void GetQueenInfo() => Console.WriteLine($"Моя королева: {_queenName}");
    }
}