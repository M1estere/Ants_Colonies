using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal class DayManager
    {
        private readonly Hike _hike = new Hike();

        private readonly Jerboa _jerboa = new Jerboa(Program.DaysBeforeDrying);
        
        internal DayManager()
        {
            InitGame();
        }
        
        protected internal void Day()
        {
            foreach (var stack in Globals.Stacks)
            {
                stack.CurrentSquads.Clear();
            }
            
            foreach (var colony in Globals.Colonies)
            {
                colony.OldPopulation[0] = colony.Workers.Count;
                colony.OldPopulation[1] = colony.Warriors.Count;
                colony.OldPopulation[2] = colony.Specials.Count;
                
                if (colony.Larvas.Count == 0)
                    colony.Queen.MakeLarvas(); // creating larvas
                
                colony.PrintColony();
            }

            Console.WriteLine();
            
            foreach (var stack in Globals.Stacks)
            {
                stack.PrintStack();
            }

            Console.WriteLine();
            
            Hike.Start(Globals.Stacks);
            
            Console.WriteLine("---------- Начало Дня ----------");
            Console.WriteLine();
            
            foreach (var stack in Globals.Stacks)
            {
                foreach (var squad in stack.CurrentSquads)
                {
                    Console.WriteLine($"С колонии {squad.SquadName} отправились на кучу {stack.Name}: {squad.StackWorkers.Count} рабочих; {squad.StackWarriors.Count} воинов; {squad.StackSpecials.Count} особенных");
                }
            }
            Console.WriteLine();
            
            _hike.StartBattle(Globals.Stacks);

            Console.WriteLine("\n---------- Конец Дня ----------\n");

            for (int j = 0; j < Globals.Colonies.Count; j++)
            {
                for (int i = 0; i < Globals.Colonies[j].Larvas.Count; i++)
                {
                    Globals.Colonies[j].Larvas[i].DecreaseGrowthDays();
                }
            }
            
            foreach (var colony in Globals.Colonies)
            {
                Console.WriteLine($"В Колонию {colony.Name} вернулись:");
                Console.WriteLine($"\tРабочих: {colony.Workers.Count - colony.NewWorkers}; \n\tВоинов: {colony.Warriors.Count - colony.NewWarriors}; \n\tОсобенных: {colony.Specials.Count}.");
                Console.WriteLine($"Потери: \n\tРабочих: {colony.OldPopulation[0] - (colony.Workers.Count - colony.NewWorkers)}; " +
                                  $"\n\tВоинов: {colony.OldPopulation[1] - (colony.Warriors.Count - colony.NewWarriors)}; " +
                                  $"\n\tОсобенных: {colony.OldPopulation[2] - colony.Specials.Count}");
                Console.WriteLine($"Добыто ресурсов: Веток: {colony.NewResources[0]}; Листьев: {colony.NewResources[2]}; Росинок: {colony.NewResources[1]}; Камней: {colony.NewResources[3]}.");
                Console.WriteLine($"Выросло личинок: \n\tРабочих: {colony.NewWorkers}; \n\tВоинов: {colony.NewWarriors}. \nОсталось личинок: {colony.Larvas.Count}.");
                
                colony.NewWorkers = 0;
                colony.NewWarriors = 0;

                colony.NewResources = new List<int>() {0, 0, 0, 0};
                
                Console.WriteLine();
            }
            
            if (_jerboa.FirstDay == Program.CurrentDays)
                Console.WriteLine("Глобальный эффект: Тушканчик съест всех муравьев рабочих в один из следующих 8 дней.");


            if (Program.CurrentDays != _jerboa.RandomDay) return;
            
            Console.WriteLine("Нападает Тушканчик!");
            Console.WriteLine("Рабочие всех колоний погибают");
            Jerboa.Action();
        }

        #region Initialization
        private static void InitGame()
        {
            InitStacks();
            
            // init first colony (red)
            Colony redColony = new Colony("Красные", 18, 9);
            Queen isabella = new Queen("Изабелла", 23, 8, 26, 3, 4, 3, 4, true, redColony);

            redColony.Queen = isabella;

            redColony.Specials.Add(new SpecialInsect(SpecialInsectRank.Hardworker, isabella, 0, 0, 0));
            
            InitColony(redColony, WorkerRank.ElderOne, WorkerRank.Elite, WorkerRank.Legend, WarriorRank.Legend, WarriorRank.Elite, WarriorRank.Elite);

            // init second colony (orange)
            Colony orangeColony = new Colony("Рыжие", 18, 6);
            Queen kleopatra = new Queen("Клеопатра", 21, 6, 21, 1, 4, 3, 5, true, orangeColony);

            orangeColony.Queen = kleopatra;

            orangeColony.Specials.Add(new SpecialInsect(SpecialInsectRank.Lazy, kleopatra, 0, 0, 0));
            
            InitColony(orangeColony, WorkerRank.ElderTwo, WorkerRank.Legend, WorkerRank.AdvancedForget, WarriorRank.Elder, WarriorRank.Elder, WarriorRank.Advanced);

            Globals.Colonies.Add(redColony);
            Globals.Colonies.Add(orangeColony);
        }

        private static void InitStacks()
        { 
            Stack firstStack = new Stack("1", 25, 0, 0, 0);
            Stack secondStack = new Stack("2", 16, 0, 0, 0);
            Stack thirdStack = new Stack("3", 12, 42, 22, 0);
            Stack fourthStack = new Stack("4", 26, 36, 20, 0);
            Stack fifthStack = new Stack("5", 24, 0, 0, 0);

            Globals.Stacks.Add(firstStack);
            Globals.Stacks.Add(secondStack);
            Globals.Stacks.Add(thirdStack);
            Globals.Stacks.Add(fourthStack);
            Globals.Stacks.Add(fifthStack);
        }

        private static void InitColony(Colony colony, WorkerRank firstWorker, WorkerRank secondWorker, WorkerRank thirdWorker, WarriorRank firstWarrior, WarriorRank secondWarrior, WarriorRank thirdWarrior)
        {
            colony.CreateAnts(new List<WorkerRank>() {firstWorker, secondWorker, thirdWorker}, 
                new List<WarriorRank>() {firstWarrior, secondWarrior, thirdWarrior});
        }
        #endregion
    }
}