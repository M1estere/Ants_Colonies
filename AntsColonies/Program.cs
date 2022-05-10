using System;
using System.Collections.Generic;
using System.Linq;

namespace AntsColonies
{
    static class Program
    {
        internal static int DaysBeforeDrying = 15;

        internal static int CurrentDays = 1;
        
        private static void BeginPrint()
        {
            Console.WriteLine($"\nДо засухи: {DaysBeforeDrying} дней\n");
        }
        
        private static void Main(string[] args)
        {
            BeginPrint();
            
            DayManager _dayManager = new DayManager();
            
            while (CurrentDays <= DaysBeforeDrying)
            {
                Console.WriteLine($"------------------- День: {CurrentDays} (осталось {DaysBeforeDrying - CurrentDays} дней) -------------------\n");
                
                _dayManager.Day();
                
                CurrentDays++;
                
                if (CurrentDays > DaysBeforeDrying)
                    Console.WriteLine("\nНажмите Enter, чтобы закончить");
                else
                    Console.WriteLine("\nНажмите Enter, чтобы начать следующий день");
                
                Console.ReadLine();
            }

            FinishSimulation();
        }

        private static void FinishSimulation()
        {
            Console.WriteLine("------------------- Результаты -------------------");
            List<int> coloniesResources = new List<int>();
            
            foreach (Colony colony in Globals.Colonies)
            {
                colony.PrintColony();
                int resources = colony.Branches + colony.Dewdrops + colony.Leaves + colony.Stones;
                coloniesResources.Add(resources);
            }

            Console.WriteLine($"------------------ Колония {Globals.Colonies[coloniesResources.IndexOf(coloniesResources.Max())].Name} выживает! ------------------");
        }
    }
}