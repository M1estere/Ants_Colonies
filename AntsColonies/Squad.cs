using System.Collections.Generic;

namespace AntsColonies
{
    public class Squad
    {
        internal string SquadName;
        
        internal List<AntWorker> StackWorkers = new List<AntWorker>();
        internal List<AntWarrior> StackWarriors = new List<AntWarrior>();

        internal int Branches;
        internal int Leaves;
        internal int Dewdrops;
        internal int Stones;
        
        public Squad(string squadName)
        {
            SquadName = squadName;
        }
    }
}