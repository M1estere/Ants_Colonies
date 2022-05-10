using System;
using System.Collections.Generic;

namespace AntsColonies
{
    public class SquadCreator
    {
        internal void FillSquad(Colony colony, List<Stack> stacks)
        {
            if (colony.Warriors.Count == 0 && colony.Workers.Count == 0)
                return;
            
            for (int k = 0; k < stacks.Count; k++)
            {
                if (stacks[k].Branches == 0 && stacks[k].Dewdrops == 0 && stacks[k].Leaves == 0 && stacks[k].Stones == 0)
                    continue; // if an empty stack then search another one
                
                Squad squad = new Squad(colony.Name, colony.Queen);
            
                int randomWorkersNumber = 0;
                int randomWarriorsNumber = 0;

                // filling squad with special insect
                if (colony.Specials.Count > 0)
                {
                    int gonotgo = Globals.Random.Next(0, 2);

                    if (gonotgo % 2 == 0)
                    {
                        if (colony.Specials[0] != null)
                        {
                            squad.StackSpecials.Add(colony.Specials[0]);
                            squad.CheckSpecial();
                            colony.Specials.Clear();
                        }
                    }
                }
                
                randomWorkersNumber = colony.Workers.Count > 0 ? Globals.Random.Next(1, colony.Workers.Count) : Globals.Random.Next(0, colony.Workers.Count);
    
                randomWarriorsNumber = colony.Warriors.Count > 0 ? Globals.Random.Next(1, colony.Warriors.Count) : Globals.Random.Next(0, colony.Warriors.Count);
            
                for (int i = 0; i < randomWorkersNumber; i++) // fillings squad with workers
                {
                    int random = Globals.Random.Next(0, colony.Workers.Count);
        
                    squad.StackWorkers.Add(colony.Workers[random]);
                    colony.Workers.RemoveAt(random);
                }
        
                for (int i = 0; i < randomWarriorsNumber; i++) // filling squad with warriors
                {
                    int random = Globals.Random.Next(0, colony.Warriors.Count);
        
                    squad.StackWarriors.Add(colony.Warriors[random]);
                    colony.Warriors.RemoveAt(random);
                }

                if (randomWorkersNumber > 0 || randomWarriorsNumber > 0) // adding squad to the current stack 
                {
                    stacks[k].CurrentSquads.Add(squad);
                }
            }
        }
    }
}
