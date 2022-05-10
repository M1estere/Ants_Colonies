using System;
using System.Collections.Generic;

namespace AntsColonies
{
    public class Colony
    {
        internal string Name;

        internal Queen Queen;

        internal static int WorkersAmount;
        internal static int WarriorsAmount;

        internal List<AntWorker> Workers = new List<AntWorker>();
        internal List<AntWarrior> Warriors = new List<AntWarrior>();
        internal List<SpecialInsect> Specials = new List<SpecialInsect>();

        internal int Branches;
        internal int Leaves;
        internal int Dewdrops;
        internal int Stones;

        internal List<Larva> Larvas = new List<Larva>();

        internal int NewWorkers = 0;
        internal int NewWarriors = 0;

        internal List<int> NewResources = new List<int>() {0, 0, 0, 0};

        internal List<int> OldPopulation = new List<int>() {0, 0, 0}; // worker warrior special
        
        public Colony(string name, int workersAmount, int warriorsAmount)
        {
            Name = name;
            
            WorkersAmount = workersAmount;
            WarriorsAmount = warriorsAmount;
            
            Workers = new List<AntWorker>();
            Warriors = new List<AntWarrior>();

            CreateCommonAnts(3, 3);
        }

        private void CreateCommonAnts(int specialWorkers, int specialWarriors)
        {
            for (int i = 0; i < (WorkersAmount - specialWorkers); i++)
            {
                Workers.Add(new AntWorker(1,0,1, Queen, WorkerRank.Common));
            }

            for (int i = 0; i < (WarriorsAmount - specialWarriors); i++)
            {
                Warriors.Add(new AntWarrior(1, 0, 1, Queen, WarriorRank.Common));
            }
        }

        internal void PrintColony()
        {
            Console.WriteLine($"\nКолония: {Name}");
            Console.WriteLine($"1) Королева: {Queen.Name}, Личинок: {Larvas.Count}");
            Console.WriteLine($"2) Ресурсы: Ветки: {Branches}; Листья: {Leaves}; Росинки: {Dewdrops}; Камни: {Stones}");
            Console.WriteLine($"3) Муравьи: {Workers.Count + Warriors.Count + Specials.Count}\n\tРабочие: {Workers.Count}; Воины: {Warriors.Count}; Особенных: {Specials.Count}\n");
        }
    }
}