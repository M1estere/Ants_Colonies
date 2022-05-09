using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal class Hike
    {
        private bool _noWinner = false;

        private bool _metFriend = false;
        
        internal void Start(List<Colony> colonies, List<Stack> stacks)
        {
            foreach (Colony colony in colonies)
            {
                SquadCreator squadCreator = new SquadCreator();

                squadCreator.FillSquad(colony, stacks);
            }
        }

        internal void StartBattle(List<Stack> stacks) // starting battle if more than 1 colony on stack
        {
            foreach (Stack stack in stacks)
            {
                if (stack.CurrentSquads.Count >= 2)
                {
                    Battle(stack, stack.CurrentSquads);
                }
            }
        }

        private void Battle(Stack stack, List<Squad> squadsHere) // battle handler
        {
<<<<<<< HEAD
            _metFriend = false;
            Console.WriteLine($"\nНачинается сражение между {squadsHere.Count} колониями на куче {stack.Name}!\n");
            
            bool finished = false; // check if battle is finished

            int tempCount = 0;
            for (int i = 0; i < squadsHere.Count; i++)
            {
                if (squadsHere[i].StackWarriors.Count == 0)
                {
                    tempCount++;
                }
            }

            if (tempCount == squadsHere.Count)
            {
                _noWinner = true;
                finished = true;
            }

            while (!finished)
            {
                List<Creature> aliveCreatures = new List<Creature>(); // reseting alive creatures
                aliveCreatures.Clear();
                
                foreach (Squad squad in squadsHere) // filling all alive creatures
                {
                    foreach (AntWarrior warrior in squad.StackWarriors) // filling warriors that are not dead
=======
            Console.WriteLine($"Начинается сражение между {squadsHere.Count} колониями!\n");
            
            while (true)
            {
                bool finished = false; // check if battle is finished
                
                List<Creature> aliveCreatures = new List<Creature>(); // reseting alive creatures
                
                for (int i = 0; i < squadsHere; i++) // filling all alive creatures
                {
                    foreach (AntWarrior warrior in squadsHere[i].StackWarriors) // filling warriors that are not dead
>>>>>>> 45647807db5c2f8efec21abc70a700949550b749
                    {
                        if (warrior.Health > 0)
                            aliveCreatures.Add(warrior);
                    }

<<<<<<< HEAD
                    foreach (AntWorker worker in squad.StackWorkers) // filling with alive workers
=======
                    foreach (AntWorker worker in squadsHere[i].StackWorkers) // filling with alive workers
>>>>>>> 45647807db5c2f8efec21abc70a700949550b749
                    {
                        if (worker.Health > 0)
                            aliveCreatures.Add(worker);
                    }
                }
                
                for (int i = 0; i < squadsHere.Count; i++) // giving every warrior its own random target
                {
<<<<<<< HEAD
                    foreach (AntWarrior warrior in squadsHere[i].StackWarriors) // attack
                    {
                        if (aliveCreatures.Count > 1)
                        {
                            for (int j = 0; j < warrior.AntsToAttack; j++)
                            {
                                Creature creature = FindTarget(aliveCreatures, squadsHere[i]);
                                if (creature != null)
                                    warrior.Attack(creature);
                            }
                        }
                    }
                }

                squadsHere = CheckUnits(squadsHere);
                for (int i = 0; i < squadsHere.Count; i++)
                {
                    if (squadsHere[i].StackWarriors.Count == 0) // one army loses
                    {
                        Console.WriteLine($"{squadsHere[i].SquadName} проигрывают сражение на куче {stack.Name}!");
                        Console.Write($"\tОтряд {squadsHere[i].SquadName}: Воинов: {squadsHere[i].StackWarriors.Count}; Рабочих: {squadsHere[i].StackWorkers.Count}; Особенных: {squadsHere[i].StackSpecials.Count}\n\n");
                        
                        finished = true;
                    }
                }
                
                aliveCreatures.Clear();

                if (_metFriend) finished = true;
            }
            FindWinner(stack, squadsHere);
            FinishHike(stack, squadsHere);
        }

        private Creature FindTarget(List<Creature> creatures, Squad squad)
        {
            bool found = false;
            Creature creature = null;

            List<Creature> newCreatures = new List<Creature>();

            List<Creature> friends = new List<Creature>();
            
            while (!found)
            {
                int randomOpponent = Globals.Random.Next(0, creatures.Count);

                if (friends.Count == creatures.Count - 1)
                {
                    found = true;
                    _metFriend = true;
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
                            }
                            else
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
        
=======
                    foreach (AntWarrior warrior in squadsHere[i].StackWarriors)
                    {
                        int randomOpponent = Globals.Random.Next(0, aliveCreatures.Count);
                        warrior.Attack(aliveCreatures[randomOpponent]);
                    }
                    if (squadsHere[i].StackWarriors.Count == 0) // one army loses
                    {
                        Console.WriteLine($"{squadsHere[i].SquadName} проигрывает сражение!");
                        Console.WriteLine("Итог:");
                        for (int j = 0; j < squadsHere.Count; j++)
                        {
                            Console.WriteLine($"\tОтряд {j+1}: Воинов: {squadsHere[j].StackWarriors}; Рабочих: {squadsHere[j].StackWorkers}")
                            finished = true;
                        }
                    }
                }
                if (finished) break;
            }
            FindWinner(stack, squadsHere);
            FinishHike(stack, CheckUnits(squadsHere));
        }

>>>>>>> 45647807db5c2f8efec21abc70a700949550b749
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
            }

            return squads;
        }

        private void FindWinner(Stack stack, List<Squad> squads) // find a winner of the hike
        {
<<<<<<< HEAD
            if (_noWinner)
            {
                foreach (Squad squad in squads)
                {
                    Console.WriteLine($"Отряд: {squad.SquadName}");
                    Console.WriteLine($"\tОтряд: Воинов: {squad.StackWarriors.Count}; Рабочих: {squad.StackWorkers.Count}; Особенных: {squad.StackSpecials.Count}");
                
                    CollectResources(squad, stack);
                
                    Console.WriteLine($"Ресурсов получено: \n\tВеток: {squad.Branches};\n\tРосинок: {squad.Dewdrops};" +
                                      $"\n\tЛистьев: {squad.Leaves};\n\tКамней: {squad.Stones}.");
                }

                return;
            }
            foreach (Squad squad in squads)
            {
                if (squad.StackWarriors.Count > 0)
                {
                    Console.WriteLine($"Победитель битвы: {squad.SquadName}");
                    Console.WriteLine($"\tОтряд: Воинов: {squad.StackWarriors.Count}; Рабочих: {squad.StackWorkers.Count}; Особенных: {squad.StackSpecials.Count}");
                    
                    CollectResources(squad, stack);
                    
                    Console.WriteLine($"Ресурсов получено: \n\tВеток: {squad.Branches};\n\tРосинок: {squad.Dewdrops};" +
                                      $"\n\tЛистьев: {squad.Leaves};\n\tКамней: {squad.Stones}.");
=======
            for (int i = 0; i < squads.Count; i++)
            {
                if (squads[i].StackWarriors > 0)
                {
                    Console.WriteLine($"Победитель битвы: {squads[i].Name}");
                    CollectResources(squads[i], stack);
>>>>>>> 45647807db5c2f8efec21abc70a700949550b749
                }
            }
        }

        private void FinishHike(Stack stack, List<Squad> squads) // going home
        {
<<<<<<< HEAD
            foreach (Colony colony in Globals.Colonies)
            {
                foreach (Squad squad in squads)
                {
                    if (squad.SquadName == colony.Name) // colony's squad
                    {
                        Finish(squad, colony);
                    }
=======
            for (int i = 0; i < Globals.Colonies.Count; i++)
            {
                for (int j = 0; j < squads.Count; j++)
                {
                    if (squads[j].Name == Globals.Colonies[i].Name) // colony's squad
                    {
                        Finish(squads[j], Globals.Colonies[i]);
                        Globals.Colonies[i].PrintColony();
                    } else
                    {
                        continue;
                    }  
>>>>>>> 45647807db5c2f8efec21abc70a700949550b749
                }
            }
            stack.CurrentSquads.Clear();
        }

        private void Finish(Squad squad, Colony colony) // remove from squad to colony
        {
            // moving resources from squad to colony
            colony.Branches += squad.Branches;
            squad.Branches = 0;

            colony.Dewdrops += squad.Dewdrops;
            squad.Dewdrops = 0;

            colony.Leaves += squad.Leaves;
            squad.Leaves = 0;

            colony.Stones += squad.Stones;
            squad.Stones = 0;
            
            for (int i = 0; i < squad.StackWarriors.Count; i++)
            {
                colony.Warriors.Add(squad.StackWarriors[i]);
<<<<<<< HEAD
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
                        Console.WriteLine($"{squad.StackSpecials[0].Name} из колонии {squad.SquadName} потерялась по пути назад!");
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
                
=======
                squad.StackWarriors.RemoveAt(i);
            }

            for (int i = 0; i < squad.StackWorkers.Count; i++)
            {
                colony.Workers.Add(squad.StackWorkers[i]);
                squad.StackWorkers.RemoveAt(i);
>>>>>>> 45647807db5c2f8efec21abc70a700949550b749
            }
        }

        private void CollectResources(Squad squad, Stack stack)
        {
            foreach (AntWorker worker in squad.StackWorkers)
            {
                worker.Collect(squad, stack);
            }
<<<<<<< HEAD

            if (squad.StackSpecials.Count > 0)
            {
                squad.StackSpecials[0].Collect(squad, stack);
            }
        } // making all workers collect resources and special insects
=======
        } // making all workers collect resources
>>>>>>> 45647807db5c2f8efec21abc70a700949550b749
    }
}