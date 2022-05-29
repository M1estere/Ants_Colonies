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
        }

        internal void CreateAnts(List<WorkerRank> workerRanks, List<WarriorRank> warriorRanks)
        {
            for (int i = 0; i < _workersAmount; i++)
            {
                Workers.Add(new AntWorker(Queen, workerRanks[Globals.Random.Next(0, workerRanks.Count)]));
            }

            for (int i = 0; i < _warriorsAmount; i++)
            {
                Warriors.Add(new AntWarrior(Queen, warriorRanks[Globals.Random.Next(0, warriorRanks.Count)]));
            }
        }

        internal void PrintColony()
        {
            Console.WriteLine($"\nКолония: {Name}");
            Console.WriteLine($"1) Королева: {Queen.Name}, Личинок: {Larvas.Count}");
            Queen.PrintFriends();
            Console.WriteLine($"2) Ресурсы: Ветки: {Branches}; Листья: {Leaves}; Росинки: {Dewdrops}; Камни: {Stones}");
            Console.WriteLine($"3) Муравьи: {Workers.Count + Warriors.Count + Specials.Count}\n\tРабочие: {Workers.Count}; Воины: {Warriors.Count}; Особенных: {Specials.Count}\n");
        }
    }
}