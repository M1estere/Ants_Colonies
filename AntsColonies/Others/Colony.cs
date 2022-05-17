using System;
using System.Collections.Generic;

namespace AntsColonies
{
    public class Colony
    {
        internal readonly string Name;

        internal Queen Queen;

        private static int _workersAmount;
        private static int _warriorsAmount;

        internal List<AntWorker> Workers;
        internal List<AntWarrior> Warriors;
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
            
            _workersAmount = workersAmount;
            _warriorsAmount = warriorsAmount;
            
            Workers = new List<AntWorker>();
            Warriors = new List<AntWarrior>();

            CreateCommonAnts(3, 3);
        }

        private void CreateCommonAnts(int specialWorkers, int specialWarriors)
        {
            for (int i = 0; i < (_workersAmount - specialWorkers); i++)
            {
                Workers.Add(new AntWorker(1,0,1, Queen, WorkerRank.Common));
            }

            for (int i = 0; i < (_warriorsAmount - specialWarriors); i++)
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