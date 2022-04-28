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

        internal void StartBattle(List<Stack> stacks)
        {
            foreach (Stack stack in stacks)
            {
                if (stack.CurrentSquads.Count >= 2)
                {
                    //Battle battle = new Battle(stack, stack.CurrentSquads[0], stack.CurrentSquads[1]);
                    Battle();
                }
            }
        }

        private void Battle()
        {
            foreach (Stack stack in Globals.Stacks)    
            {
                if (stack.CurrentSquads.Count >= 2)
                {
                    // battle
                    while (1 != 0)
                    {
                        
                    }
                }
            }
        }
    }
}