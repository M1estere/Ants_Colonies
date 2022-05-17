using System;
using System.Collections.Generic;
using System.Linq;

namespace AntsColonies
{
    internal static class Program
    {
        internal const int DaysBeforeDrying = 15;

        internal static int CurrentDays = 1;
        
        private static void BeginPrint()
        {
            Console.WriteLine($"\nДо засухи: {DaysBeforeDrying} дней\n");
        }
        
        private static void Main()
        {
            BeginPrint();
            
            DayManager dayManager = new DayManager();
            
            while (CurrentDays <= DaysBeforeDrying)
            {
                Console.WriteLine($"------------------- День: {CurrentDays} (осталось {DaysBeforeDrying - CurrentDays} дней) -------------------\n");
                
                dayManager.Day();
                
                CurrentDays++;

                Console.WriteLine(CurrentDays > DaysBeforeDrying ? "\nНажмите Enter, чтобы закончить"
                    : "\nНажмите Enter, чтобы начать следующий день");

                Console.ReadLine();
            }

            FinishSimulation();
        }

        private static void FinishSimulation()
        {
            Console.WriteLine("------------------- Результаты -------------------");
            List<int> coloniesResources = new List<int>();
            
            foreach (var colony in Globals.Colonies)
            {
                colony.PrintColony();
                
                int resources = colony.Branches + colony.Dewdrops + colony.Leaves + colony.Stones;
                coloniesResources.Add(resources);
            }

            Console.WriteLine($"------------------ Колония {Globals.Colonies[coloniesResources.IndexOf(coloniesResources.Max())].Name} выживает! ------------------");

            Console.WriteLine("\nНажмите Enter чтобы закрыть симуляцию");
            Console.ReadLine();
        }
    }
}