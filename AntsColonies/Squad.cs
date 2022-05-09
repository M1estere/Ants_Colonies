using System;
using System.Collections.Generic;

namespace AntsColonies
{
    public class Squad
    {
        internal string SquadName;

        internal Queen SquadQueen;
        
        internal List<AntWorker> StackWorkers = new List<AntWorker>();
        internal List<AntWarrior> StackWarriors = new List<AntWarrior>();

        internal List<SpecialInsect> StackSpecials = new List<SpecialInsect>();
        
        internal int Branches;
        internal int Leaves;
        internal int Dewdrops;
        internal int Stones;
        
        public Squad(string squadName, Queen queen)
        {
            SquadQueen = queen;
            SquadName = squadName;
        }

        internal void CheckSpecial()
        {
            if (StackSpecials[0].Rank == SpecialInsectRank.Lazy)
            {
                foreach (AntWorker worker in StackWorkers)
                {
                    worker.Health += worker.Protection;
                }
                foreach (AntWarrior warrior in StackWarriors)
                {
                    warrior.Health += warrior.Protection;
                }
            }
        }
    }
}