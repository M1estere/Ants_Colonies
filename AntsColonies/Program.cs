using System;
using System.Collections.Generic;
using System.Linq;

namespace AntsColonies
{
    static class Program
    {
        private static int _daysBeforeDrying = 15;

        private static int _currentDays = 1;
        
        private static void BeginPrint()
        {
            Console.WriteLine($"\nДо засухи: {_daysBeforeDrying} дней");
            Console.WriteLine("\n---------------------------------------------\n");
        }
        
        private static void Main(string[] args)
        {
            BeginPrint();
            
            DayManager _dayManager = new DayManager();
            
            while (_currentDays <= _daysBeforeDrying)
            {
                Console.WriteLine($"------------------- День: {_currentDays} -------------------");
                _dayManager.Day();
                _currentDays++;
                if (_currentDays >= _daysBeforeDrying)
                    Console.WriteLine("\nНажмите Enter, чтобы закончить");
                else
                    Console.WriteLine("\nНажмите Enter, чтобы начать следующий день");
                Console.ReadLine();
            }

            FinishSimulation();
        }

        private static void FinishSimulation()
        {
            List<int> coloniesResources = new List<int>();
            
            foreach (Colony colony in Globals.Colonies)
            {
                int resources = colony.Branches + colony.Dewdrops + colony.Leaves + colony.Stones;
                coloniesResources.Add(resources);
            }

            Console.WriteLine($"------------------{Globals.Colonies[coloniesResources.IndexOf(coloniesResources.Max())].Name} выживают!!!!------------------");
        }
    }
}