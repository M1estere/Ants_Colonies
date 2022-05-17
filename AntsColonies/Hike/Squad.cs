using System.Collections.Generic;

namespace AntsColonies
{
    public class Squad
    {
        internal readonly string SquadName;

        internal readonly Queen SquadQueen;
        
        internal readonly List<AntWorker> StackWorkers = new List<AntWorker>();
        internal readonly List<AntWarrior> StackWarriors = new List<AntWarrior>();

        internal readonly List<SpecialInsect> StackSpecials = new List<SpecialInsect>();
        
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
            if (StackSpecials[0].Rank != SpecialInsectRank.Lazy) return;
            
            foreach (var worker in StackWorkers)
            {
                worker.Health += worker.Protection;
            }
            foreach (var warrior in StackWarriors)
            {
                warrior.Health += warrior.Protection;
            }
        }
    }
}