using System.Collections.Generic;

namespace AntsColonies
{
    public class SquadCreator
    {
        internal static void FillSquad(Colony colony, List<Stack> stacks)
        {
            if (colony.Warriors.Count == 0 && colony.Workers.Count == 0)
                return;

            foreach (var stack in stacks)
            {
                if (stack.Branches == 0 && stack.Dewdrops == 0 && stack.Leaves == 0 && stack.Stones == 0)
                    continue; // if an empty stack then search another one
                
                Squad squad = new Squad(colony.Name, colony.Queen);

                // filling squad with special insect
                if (colony.Specials.Count > 0)
                {
                    int goNotNo = Globals.Random.Next(0, 2);
                    if (goNotNo % 2 == 0)
                    {
                        if (colony.Specials[0] != null)
                        {
                            squad.StackSpecials.Add(colony.Specials[0]);
                            squad.CheckSpecial();
                            colony.Specials.Clear();
                        }
                    }
                }
                
                int randomWorkersNumber = colony.Workers.Count > 0 ? Globals.Random.Next(1, colony.Workers.Count) : Globals.Random.Next(0, colony.Workers.Count);
    
                int randomWarriorsNumber = colony.Warriors.Count > 0 ? Globals.Random.Next(1, colony.Warriors.Count) : Globals.Random.Next(0, colony.Warriors.Count);
            
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
                    stack.CurrentSquads.Add(squad);
                }
            }
        }
    }
}
