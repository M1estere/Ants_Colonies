using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal class Hike
    {
        private bool _noWinner = false;

        private bool _metFriend = false;
        
        internal void Start(List<Stack> stacks)
        {
            foreach (Colony colony in Globals.Colonies)
            {
                SquadCreator squadCreator = new SquadCreator();

                squadCreator.FillSquad(colony, stacks);
            }
        }

        internal void StartBattle(List<Stack> stacks) // starting battle if more than 1 colony on stack
        {
            foreach (Stack stack in Globals.Stacks)
            {
                if (stack.CurrentSquads.Count >= 2)
                {
                    Battle(stack, stack.CurrentSquads);
                } else
                {
                    if (stack.CurrentSquads.Count > 0)
                    {
                        Console.WriteLine($"\nНа куче {stack.Name} только одна колония\n");
                        CollectResources(stack.CurrentSquads[0], stack);
                        FinishHike(stack, stack.CurrentSquads);
                    }
                }
            }
        }

        private void Battle(Stack stack, List<Squad> squadsHere) // battle handler
        {
            _noWinner = false;
            _metFriend = false;
            
            bool finished = false; // check if battle is finished

            int tempCount = 0;
            for (int i = 0; i < squadsHere.Count; i++)
            {
                if (squadsHere[i].StackWarriors.Count == 0)
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
                
                List<Creature> withSpecials = new List<Creature>();
                
                foreach (Squad squad in squadsHere)
                {
                    foreach (AntWarrior warrior in squad.StackWarriors) // filling warriors that are not dead
                    {
                        if (warrior.Health > 0)
                            aliveCreatures.Add(warrior);
                    }

                    foreach (AntWorker worker in squad.StackWorkers) // filling with alive workers
                    {
                        if (worker.Health > 0)
                            aliveCreatures.Add(worker);
                    }
                }

                withSpecials = aliveCreatures;
                
                foreach (Squad squad in squadsHere) // filling all alive creatures
                {
                    foreach (SpecialInsect special in squad.StackSpecials) // filling specials that are not dead
                    {
                        if (special.Health > 0)
                            withSpecials.Add(special);
                    }
                }
                
                for (int i = 0; i < squadsHere.Count; i++) // giving every warrior its own random target
                {
                    int beatCount = 0;
                    foreach (AntWarrior warrior in squadsHere[i].StackWarriors) // attack
                    {
                        if (aliveCreatures.Count > 1)
                        {
                            for (int j = 0; j < warrior.AntsToAttack; j++)
                            {
                                if (warrior.Rank == WarriorRank.Elite && beatCount == 0)
                                    warrior.CanOneShot = true;
                                
                                Creature creature = FindTarget(withSpecials, aliveCreatures, squadsHere[i], warrior);
                                if (creature != null)
                                    warrior.Attack(creature);
                            }
                        }

                        beatCount++;
                    }
                }

                squadsHere = CheckUnits(squadsHere);
                for (int i = 0; i < squadsHere.Count; i++)
                {
                    if (squadsHere[i].StackWarriors.Count == 0) // one army loses
                    {
                        finished = true;
                    }
                }
                
                aliveCreatures.Clear();
                withSpecials.Clear();
                
                if (_metFriend) finished = true;
            }
            FindWinner(stack, squadsHere);
            FinishHike(stack, squadsHere);
        }

        private Creature FindTarget(List<Creature> withSpecials, List<Creature> creatures, Squad squad, AntWarrior warrior)
        {
            if (warrior.Rank == WarriorRank.Advanced || warrior.Rank == WarriorRank.Elite) // могут атаковать особое
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
        
        private List<Squad> CheckUnits(List<Squad> squads) // check dead units in all squads
        {
            foreach (Squad squad in squads)
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
                foreach (Squad squad in squads)
                {
                    CollectResources(squad, stack);
                }

                return;
            }
            foreach (Squad squad in squads)
            {
                if (squad.StackWarriors.Count > 0)
                {
                    Console.WriteLine($"На куче {stack.Name} победили {squad.SquadName}, остались в живых: {squad.StackWorkers.Count} " +
                                      $"рабочих; {squad.StackWarriors.Count} воинов; {squad.StackSpecials.Count} особенных.");
                    CollectResources(squad, stack);
                }
            }
        }

        private void FinishHike(Stack stack, List<Squad> squads) // going home
        {
            foreach (Colony colony in Globals.Colonies)
            {
                foreach (Squad squad in squads)
                {
                    if (squad.SquadName == colony.Name) // colony's squad
                    {
                        Finish(squad, colony);
                    }
                }
            }
        }

        private void Finish(Squad squad, Colony colony) // remove from squad to colony
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
            
            for (int i = 0; i < squad.StackWarriors.Count; i++)
            {
                colony.Warriors.Add(squad.StackWarriors[i]);
            }

            squad.StackWarriors.Clear();
            
            for (int i = 0; i < squad.StackWorkers.Count; i++)
            {
                colony.Workers.Add(squad.StackWorkers[i]);
            }

            squad.StackWorkers.Clear();

            if (squad.StackSpecials.Count > 0)
            {
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
        }

        private void CollectResources(Squad squad, Stack stack)
        {
            foreach (AntWorker worker in squad.StackWorkers)
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