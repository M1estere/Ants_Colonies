﻿using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal class Colony
    {
        internal string Name { get; set; }
        
        internal Queen Queen { get; set; }

        internal static int WorkersAmount;
        internal static int WarriorsAmount;
        internal static int SpecialAmount;

        internal List<AntWorker> Workers = new List<AntWorker>();
        internal List<AntWarrior> Warriors = new List<AntWarrior>();
        internal List<SpecialInsect> Specials = new List<SpecialInsect>();

        internal int Branches;
        internal int Leaves;
        internal int Dewdrops;
        internal int Stones;
        
        public Colony(Queen queen, string name, int workersAmount, int warriorsAmount, int specialAmount)
        {
            Queen = queen;
            Name = name;
            
            WorkersAmount = workersAmount;
            WarriorsAmount = warriorsAmount;
            SpecialAmount = specialAmount;
            
            Workers = new List<AntWorker>();
            Warriors = new List<AntWarrior>();
            Specials = new List<SpecialInsect>();

            CreateCommonAnts(3, 3);
        }

        private void CreateCommonAnts(int specialWorkers, int specialWarriors)
        {
            for (int i = 0; i < (WorkersAmount - specialWorkers); i++)
            {
                Workers.Add(new AntWorker(1,0,1, Queen.Name, CreatureRank.Common));
            }

            for (int i = 0; i < (WarriorsAmount - specialWarriors); i++)
            {
                Warriors.Add(new AntWarrior(0, 0, 0, Queen.Name, CreatureRank.Common));
            }
        }

        internal void PrintColony()
        {
            Console.WriteLine($"Колония: {Name}\n");
            Console.WriteLine($"Муравьи: {Workers.Count + Warriors.Count + Specials.Count}\nОбычные: {Workers.Count}; Воины: {Warriors.Count}; Особенных: {Specials.Count}");
            Console.WriteLine($"\nКоролева: {Queen.Name}");
            Console.WriteLine($"\nРесурсы: Ветки: {Branches}; Листья: {Leaves}; Росинки: {Dewdrops}; Камни: {Stones}");
            Console.WriteLine("\n---------------------------------------------\n");
        }
    }
}