using System;

namespace AntsColonies
{
    public class AntWorker : Creature
    {
        private readonly WorkerRank _rank;

        public AntWorker(int health, int protection, int damage, Queen queen, WorkerRank rank) : base(queen, health, protection, damage)
        {
            Queen = queen;
            _rank = rank;
            switch (rank)
            {
                case WorkerRank.Common:
                    Health = 1;
                    Protection = 0;
                    break;
                case WorkerRank.ElderOne:
                    Health = 2;
                    Protection = 1;
                    break;
                case WorkerRank.ElderTwo:
                    Health = 2;
                    Protection = 1;
                    break;
                case WorkerRank.Elite:
                    Health = 8;
                    Protection = 4;
                    break;
                case WorkerRank.Legend:
                    Health = 10;
                    Protection = 6;
                    break;
                case WorkerRank.LegendTalented:
                    Health = 10;
                    Protection = 6;
                    break;
                case WorkerRank.AdvancedForget:
                    Health = 6;
                    Protection = 6;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(rank), rank, null);
            }

            Damage = 0;
            Health += Protection;
        }

        internal void Collect(Squad squad, Stack stack)
        {
            switch (_rank)
            {
                case WorkerRank.Common:
                    int random = Globals.Random.Next(0, 4);
                    switch (random)
                    {
                        case 0:
                            if (stack.Branches > 0)
                            {
                                stack.Branches--;
                                squad.Branches++;
                            }
                            break;
                        case 1:
                            if (stack.Leaves > 0)
                            {
                                stack.Leaves--;
                                squad.Leaves++;
                            }
                            break;
                        case 2:
                            if (stack.Dewdrops > 0)
                            {
                                stack.Dewdrops--;
                                squad.Dewdrops++;
                            }
                            break;
                        case 3:
                            if (stack.Stones > 0)
                            {
                                stack.Stones--;
                                squad.Stones++;
                            }
                            break;
                    }
                    break;
                case WorkerRank.ElderOne:
                    int randomResourceOne = Globals.Random.Next(0, 2);
                    if (randomResourceOne % 2 == 0)
                    {
                        if (stack.Stones > 0)
                        {
                            stack.Stones--;
                            squad.Stones++;
                        }
                    } else
                    {
                        if (stack.Branches > 0)
                        {
                            stack.Branches--;
                            squad.Branches++;
                        }
                    }
                    break;
                case WorkerRank.ElderTwo:
                    if (stack.Branches > 0)
                    {
                        stack.Branches--;
                        squad.Branches++;
                    }
                    break;
                case WorkerRank.Elite:
                    if (stack.Branches > 0)
                    {
                        stack.Branches--;
                        squad.Branches++;
                    }

                    if (stack.Dewdrops > 0)
                    {
                        stack.Dewdrops--;
                        squad.Dewdrops++;
                    }
                    break;
                case WorkerRank.Legend:
                    if (stack.Stones > 1)
                    {
                        stack.Stones -= 2;
                        squad.Stones += 2;
                    } else if (stack.Stones == 1)
                    {
                        stack.Stones--;
                        squad.Stones++;
                    }
                    
                    if (stack.Branches > 0)
                    {
                        stack.Branches--;
                        squad.Branches++;
                    }
                    break;
                case WorkerRank.LegendTalented: // всегда находит ресурсы в куче
                    if (stack.Stones > 1)
                    {
                        stack.Stones -= 2;
                        squad.Stones += 2;
                    } else
                        squad.Stones += 2;
                    
                    if (stack.Leaves > 1)
                    {
                        stack.Leaves -= 2;
                        squad.Leaves += 2;
                    } else
                        squad.Leaves += 2;
                    break;
                case WorkerRank.AdvancedForget:
                    int takeOrNot = Globals.Random.Next(0, 2); // забыл или нет
                    if (takeOrNot % 2 == 0)
                    {
                        int randomResource = Globals.Random.Next(0, 2);
                        if (randomResource % 2 == 0)
                        {
                            if (stack.Stones > 0)
                            {
                                stack.Stones--;
                                squad.Stones++;
                            }
                        } else
                        {
                            if (stack.Leaves > 0)
                            {
                                stack.Leaves--;
                                squad.Leaves++;
                            }
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        internal override void GetInfo()
        {
            Console.WriteLine("\n---------------------------------------------\n");
            Console.WriteLine($"Муравей - Рабочий ({_rank})\nЗдоровье: {Health}; Защита: {Protection}; Урон: {Damage}");
            Console.WriteLine("\n---------------------------------------------\n");
        }
        
        internal void GetQueenInfo() => Console.WriteLine($"Моя королева: {Queen.Name}");
    }
}