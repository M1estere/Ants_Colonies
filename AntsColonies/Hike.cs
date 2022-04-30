using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal class Hike
    {
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
            Console.WriteLine($"Начинается сражение между {squadsHere.Count} колониями!\n");
            
            while (true)
            {
                bool finished = false; // check if battle is finished
                
                List<Creature> aliveCreatures = new List<Creature>(); // reseting alive creatures
                
                for (int i = 0; i < squadsHere; i++) // filling all alive creatures
                {
                    foreach (AntWarrior warrior in squadsHere[i].StackWarriors) // filling warriors that are not dead
                    {
                        if (warrior.Health > 0)
                            aliveCreatures.Add(warrior);
                    }

                    foreach (AntWorker worker in squadsHere[i].StackWorkers) // filling with alive workers
                    {
                        if (worker.Health > 0)
                            aliveCreatures.Add(worker);
                    }
                }
                
                for (int i = 0; i < squadsHere.Count; i++) // giving every warrior its own random target
                {
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
            for (int i = 0; i < squads.Count; i++)
            {
                if (squads[i].StackWarriors > 0)
                {
                    Console.WriteLine($"Победитель битвы: {squads[i].Name}");
                    CollectResources(squads[i], stack);
                }
            }
        }

        private void FinishHike(Stack stack, List<Squad> squads) // going home
        {
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
                squad.StackWarriors.RemoveAt(i);
            }

            for (int i = 0; i < squad.StackWorkers.Count; i++)
            {
                colony.Workers.Add(squad.StackWorkers[i]);
                squad.StackWorkers.RemoveAt(i);
            }
        }

        private void CollectResources(Squad squad, Stack stack)
        {
            foreach (AntWorker worker in squad.StackWorkers)
            {
                worker.Collect(squad, stack);
            }
        } // making all workers collect resources
    }
}