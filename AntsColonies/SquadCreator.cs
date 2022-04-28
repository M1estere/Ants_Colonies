using System;
using System.Collections.Generic;

namespace AntsColonies
{
    public class SquadCreator
    {
        internal void FillSquad(Colony colony, List<Stack> stacks)
        {
            for (int k = 0; k < stacks.Count; k++)
            {
                if (stacks[k].Branches == 0 && stacks[k].Dewdrops == 0 && stacks[k].Leaves == 0 &&
                    stacks[k].Stones == 0)
                    continue;
                
                Squad squad = new Squad(colony.Name);
            
                int randomWorkersNumber;
                int randomWarriorsNumber;
    
                randomWorkersNumber = colony.Workers.Count > 0 ? Globals.Random.Next(1, colony.Workers.Count) : Globals.Random.Next(0, colony.Workers.Count);
    
                randomWarriorsNumber = colony.Warriors.Count > 0 ? Globals.Random.Next(1, colony.Warriors.Count) : Globals.Random.Next(0, colony.Warriors.Count);
            
                for (int i = 0; i < randomWorkersNumber; i++)
                {
                    int random = Globals.Random.Next(0, colony.Workers.Count);
        
                    squad.StackWorkers.Add(colony.Workers[random]);
                    colony.Workers.RemoveAt(random);
                }
        
                for (int i = 0; i < randomWarriorsNumber; i++)
                {
                    int random = Globals.Random.Next(0, colony.Warriors.Count);
        
                    squad.StackWarriors.Add(colony.Warriors[random]);
                    colony.Warriors.RemoveAt(random);
                }
    
                if (randomWorkersNumber > 0 || randomWarriorsNumber > 0)
                    stacks[k].CurrentSquads.Add(squad);
            }
        }
    }
}
