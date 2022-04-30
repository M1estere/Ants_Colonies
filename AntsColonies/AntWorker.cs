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

        internal void Collect(Squad squad, Stack stack)
        { 
            int randomResource = Globals.Random.Next(0, 5);
            switch (randomResource) // grabbing random resource
            {
                case 0:
                    if (stack.Branches != 0)
                    {
                        stack.Branches--;
                        squad.Branches++;
                        break;
                    } else
                    {
                        randomResource = Globals.Random.Next(0, 5); // choosing again
                        break;
                    }
                case 1:
                    if (stack.Branches != 0)
                    {
                        stack.Dewdrops--;
                        squad.Dewdrops++;
                        break;
                    } else
                    {
                        randomResource = Globals.Random.Next(0, 5);
                        break;
                    }
                case 2:
                    if (stack.Branches != 0)
                    {
                        stack.Leaves--;
                        squad.Leaves++;
                        break;
                    } else
                    {
                        randomResource = Globals.Random.Next(0, 5);
                        break;
                    }
                case 3:
                    if (stack.Branches != 0)
                    {
                        stack.Stones--;
                        squad.Stones++;
                        break;
                    } else
                    {
                        randomResource = Globals.Random.Next(0, 5);
                        break;
                    }
                default:
                    break;
            }
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