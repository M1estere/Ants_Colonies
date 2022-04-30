using System;

namespace AntsColonies
{
    
    static class Program
    {
        private static int _daysBeforeDrying = 15;

        private static void BeginPrint()
        {
            Console.WriteLine($"\nДо засухи: {_daysBeforeDrying} дней");
            Console.WriteLine("\n---------------------------------------------\n");
        }
        
        private static void Main(string[] args)
        {
            BeginPrint();
            
            DayManager _dayManager = new DayManager();
            
            while (_daysBeforeDrying > 0)
            {
                Console.WriteLine($"------------------- День: {Math.Abs(_daysBeforeDrying - 16)} -------------------");
                _dayManager.Day();
                _daysBeforeDrying--;
                Console.WriteLine("\nНажмите Enter, чтобы начать следующий день");
                Console.ReadLine();
            }
        }
    }
}