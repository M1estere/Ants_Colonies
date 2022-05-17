using System;
using System.Collections.Generic;
using System.Linq;

namespace AntsColonies
{
    internal class Hike
    {
        private bool _noWinner;

        private bool _metFriend;
        
        internal static void Start(List<Stack> stacks)
        {
            foreach (var colony in Globals.Colonies)
            {
                SquadCreator.FillSquad(colony, stacks);
            }
        }

        internal void StartBattle(List<Stack> stacks) // starting battle if more than 1 colony on stack
        {
            foreach (var stack in Globals.Stacks)
            {
                switch (stack.CurrentSquads.Count)
                {
                    case >= 2:
                        Battle(stack, stack.CurrentSquads);
                        break;
                    case > 0:
                        Console.WriteLine($"\nНа куче {stack.Name} только одна колония\n");
                        CollectResources(stack.CurrentSquads[0], stack);
                        FinishHike(stack.CurrentSquads);
                        break;
                }
            }
        }

        private void Battle(Stack stack, List<Squad> squadsHere) // battle handler
        {
            _noWinner = false;
            _metFriend = false;
            
            bool finished = false; // check if battle is finished

            int tempCount = 0;
            foreach (var squad in squadsHere)
            {
                if (squad.StackWarriors.Count == 0)
                {
                    tempCount++;
                }
            }

            if (tempCount == squadsHere.Count) // all colonies have 0 warriors
            {
                _noWinner = true;
                finished = true;
            }

            while (!finished)
            {
                List<Creature> aliveCreatures = new List<Creature>(); // reseting alive creatures
                aliveCreatures.Clear();

                foreach (var squad in squadsHere)
                {
                    foreach (var warrior in squad.StackWarriors) // filling warriors that are not dead
                    {
                        if (warrior.Health > 0)
                            aliveCreatures.Add(warrior);
                    }

                    foreach (var worker in squad.StackWorkers) // filling with alive workers
                    {
                        if (worker.Health > 0)
                            aliveCreatures.Add(worker);
                    }
                }

                List<Creature> withSpecials = aliveCreatures;
                
                foreach (var squad in squadsHere) // filling all alive creatures
                {
                    foreach (var special in squad.StackSpecials) // filling specials that are not dead
                    {
                        if (special.Health > 0)
                            withSpecials.Add(special);
                    }
                }
                
                List<Squad> sortedSquads = squadsHere.OrderBy(_ => Globals.Random.Next()).ToList(); // to randomise attack order
                
                foreach (var squad in sortedSquads)
                {
                    int beatCount = 0;
                    foreach (var warrior in squad.StackWarriors) // attack
                    {
                        if (aliveCreatures.Count > 1)
                        {
                            for (int j = 0; j < warrior.AntsToAttack; j++)
                            {
                                if (warrior.Rank == WarriorRank.Elite && beatCount == 0)
                                    warrior.CanOneShot = true;
                                
                                Creature creature = FindTarget(withSpecials, aliveCreatures, squad, warrior);
                                if (creature != null)
                                    warrior.Attack(creature);
                            }
                        }

                        beatCount++;
                    }
                }

                squadsHere = CheckUnits(squadsHere);
                foreach (var squad in squadsHere)
                {
                    if (squad.StackWarriors.Count == 0) // one army loses
                    {
                        finished = true;
                    }
                }
                
                aliveCreatures.Clear();
                withSpecials.Clear();
                
                if (_metFriend) finished = true;
            }
            FindWinner(stack, squadsHere);
            FinishHike(squadsHere);
        }

        private Creature FindTarget(List<Creature> withSpecials, List<Creature> creatures, Squad squad, AntWarrior warrior)
        {
            if (warrior.Rank is WarriorRank.Advanced or WarriorRank.Elite) // могут атаковать особое
            {
                creatures = withSpecials; // выбирают цель вообще из всех
            }
            
            bool found = false;
            Creature creature = null;

            List<Creature> newCreatures = new List<Creature>();

            List<Creature> friends = new List<Creature>();
            
            while (!found)
            {
                int randomOpponent = Globals.Random.Next(0, creatures.Count);

                if (friends.Count == creatures.Count - 1)
                {
                    _metFriend = true;
                    found = true;
                }

                if (!squad.SquadQueen.FriendQueens.Contains(creatures[randomOpponent].Queen))
                {
                    if (creatures[randomOpponent].Health > 0)
                    {
                        if (creatures[randomOpponent] is AntWorker)
                        {
                            if (!squad.StackWorkers.Contains((AntWorker) creatures[randomOpponent]))
                            {
                                creature = creatures[randomOpponent];
                                found = true;
                            } else
                            {
                                newCreatures.Add(creatures[randomOpponent]);
                            }
                        }


                        if (creatures[randomOpponent] is AntWarrior)
                        {
                            if (!squad.StackWarriors.Contains((AntWarrior) creatures[randomOpponent]))
                            {
                                creature = creatures[randomOpponent];
                                found = true;
                            }
                            else
                            {
                                newCreatures.Add(creatures[randomOpponent]);
                            }
                        }
                    }

                    if (newCreatures.Count == creatures.Count - 1)
                    {
                        found = true;
                    }
                } else
                {
                    friends.Add(creatures[randomOpponent]);
                }
            }
            
            return creature;
        }
        
        private static List<Squad> CheckUnits(List<Squad> squads) // check dead units in all squads
        {
            foreach (var squad in squads)
            {
                for (int i = 0; i < squad.StackWarriors.Count; i++) // remove dead warriors
                {
                    if (squad.StackWarriors[i].Health <= 0)
                        squad.StackWarriors.RemoveAt(i);
                }

                for (int i = 0; i < squad.StackWorkers.Count; i++) // remove dead workers
                {
                    if (squad.StackWorkers[i].Health <= 0)
                        squad.StackWorkers.RemoveAt(i);
                }

                for (int i = 0; i < squad.StackSpecials.Count; i++)
                {
                    if (squad.StackSpecials[i].Health <= 0)
                        squad.StackSpecials.RemoveAt(i);
                }
            }

            return squads;
        }

        private void FindWinner(Stack stack, List<Squad> squads) // find a winner of the hike
        {
            if (_noWinner)
            {
                Console.WriteLine($"На куче {stack.Name} в {squads.Count} отрядах нет воинов -> они разошлись мирно!");
                foreach (var squad in squads)
                {
                    CollectResources(squad, stack);
                }

                return;
            }

            foreach (var squad in squads.Where(squad => squad.StackWarriors.Count > 0))
            {
                Console.WriteLine($"На куче {stack.Name} победили {squad.SquadName}, остались в живых: {squad.StackWorkers.Count} " +
                                  $"рабочих; {squad.StackWarriors.Count} воинов; {squad.StackSpecials.Count} особенных.");
                CollectResources(squad, stack);
            }
        }

        private static void FinishHike(List<Squad> squads) // going home
        {
            foreach (var colony in Globals.Colonies)
            {
                foreach (var squad in squads.Where(squad => squad.SquadName == colony.Name))
                {
                    Finish(squad, colony);
                }
            }
        }

        private static void Finish(Squad squad, Colony colony) // remove from squad to colony
        {
            // moving resources from squad to colony
            colony.NewResources[0] += squad.Branches;
            colony.Branches += squad.Branches;
            squad.Branches = 0;

            colony.NewResources[1] += squad.Dewdrops;
            colony.Dewdrops += squad.Dewdrops;
            squad.Dewdrops = 0;
            
            colony.NewResources[2] += squad.Leaves;
            colony.Leaves += squad.Leaves;
            squad.Leaves = 0;

            colony.NewResources[3] += squad.Stones;
            colony.Stones += squad.Stones;
            squad.Stones = 0;
            
            foreach (var warrior in squad.StackWarriors)
            {
                colony.Warriors.Add(warrior);
            }

            squad.StackWarriors.Clear();
            
            foreach (var worker in squad.StackWorkers)
            {
                colony.Workers.Add(worker);
            }

            squad.StackWorkers.Clear();

            if (squad.StackSpecials.Count <= 0) return;
            
            if (squad.StackSpecials[0].Rank == SpecialInsectRank.Hardworker)
            {
                int lostOrNot = Globals.Random.Next(0, 7);
                if (lostOrNot > 5) // потерялся
                {
                    squad.StackSpecials.Clear();
                } else // дошел
                {
                    colony.Specials.Add(squad.StackSpecials[0]);
                    squad.StackSpecials.Clear();
                }
            } else // не теряется
            {
                colony.Specials.Add(squad.StackSpecials[0]);
                squad.StackSpecials.Clear();
            }
        }

        private static void CollectResources(Squad squad, Stack stack)
        {
            foreach (var worker in squad.StackWorkers)
            {
                worker.Collect(squad, stack);
            }

            if (squad.StackSpecials.Count > 0)
            {
                squad.StackSpecials[0].Collect(squad, stack);
            }
        } // making all workers and special insects collect resources 
    }
}