using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal class DayManager
    {
        internal List<Stack> Stacks = new List<Stack>();
        
        internal Hike hike = new Hike();

        private Jerboa jerboa = new Jerboa(Program.DaysBeforeDrying);
        
        internal DayManager()
        {
            InitGame();
        }
        
        protected internal void Day()
        {
            foreach (Stack stack in Globals.Stacks)
            {
                stack.CurrentSquads.Clear();
            }
            
            foreach (Colony colony in Globals.Colonies)
            {
                colony.OldPopulation[0] = colony.Workers.Count;
                colony.OldPopulation[1] = colony.Warriors.Count;
                colony.OldPopulation[2] = colony.Specials.Count;
                
                if (colony.Larvas.Count == 0)
                    colony.Queen.MakeLarvas(); // creating larvas
                
                colony.PrintColony();
            }

            Console.WriteLine();
            
            foreach (Stack stack in Globals.Stacks)
            {
                stack.PrintStack();
            }

            Console.WriteLine();
            
            hike.Start(Globals.Stacks);
            
            Console.WriteLine("---------- Начало Дня ----------");
            Console.WriteLine();
            
            foreach (Stack stack in Globals.Stacks)
            {
                foreach (Squad squad in stack.CurrentSquads)
                {
                    Console.WriteLine($"С колонии {squad.SquadName} отправились на кучу {stack.Name}: {squad.StackWarriors.Count} воинов; {squad.StackWorkers.Count} рабочих; {squad.StackSpecials.Count} особенных");
                }
            }
            Console.WriteLine();
            
            hike.StartBattle(Globals.Stacks);

            Console.WriteLine("\n---------- Конец Дня ----------\n");
            
            for (int j = 0; j < Globals.Colonies.Count; j++)
            {
                for (int i = 0; i < Globals.Colonies[j].Larvas.Count; i++)
                {
                    Globals.Colonies[j].Larvas[i].DecreaseGrowthDays();
                }
            }
            
            foreach (Colony colony in Globals.Colonies)
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
            
            if (jerboa.FirstDay == Program.CurrentDays)
                Console.WriteLine("Глобальный эффект: Тушканчик c эффектом съедает всех муравьев рабочих в один день (в течение 8 дней)");

            
            if (Program.CurrentDays == jerboa.RandomDay)
            {
                Console.WriteLine("Нападает Тушканчик!");
                Console.WriteLine("Рабочие всех колоний погибают");
                jerboa.Action();
            }
        }

        #region Initialization
        protected internal void InitGame()
        {
            InitStacks();
            
            // init first colony (red)
            Colony _redColony = new Colony("Красные", 18, 9);
            Queen Isabella = new Queen("Изабелла", 23, 8, 26, 3, 4, 3, 4, true, _redColony);

            _redColony.Queen = Isabella;

            _redColony.Specials.Add(new SpecialInsect("Оса", SpecialInsectRank.Hardworker, Isabella, 0, 0, 0));
            
            InitColony(_redColony, WorkerRank.ElderOne, WorkerRank.Elite, WorkerRank.Legend, WarriorRank.Legend, WarriorRank.Elite, WarriorRank.Elite);
            //_redColony.PrintColony();
            
            // init second colony (orange)
            Colony _orangeColony = new Colony("Рыжие", 18, 6);
            Queen Kleopatra = new Queen("Клеопатра", 21, 6, 21, 1, 4, 3, 5, true, _orangeColony);

            _orangeColony.Queen = Kleopatra;

            _orangeColony.Specials.Add(new SpecialInsect("Толстоножка", SpecialInsectRank.Lazy, Kleopatra, 0, 0, 0));
            
            InitColony(_orangeColony, WorkerRank.ElderTwo, WorkerRank.Legend, WorkerRank.AdvancedForget, WarriorRank.Elder, WarriorRank.Elder, WarriorRank.Advanced);
            //_orangeColony.PrintColony();
            
            Globals.Colonies.Add(_redColony);
            Globals.Colonies.Add(_orangeColony);
        }

        private void InitStacks()
        { 
            Stack _firstStack = new Stack("1", 25, 0, 0, 0);
            Stack _secondStack = new Stack("2", 16, 0, 0, 0);
            Stack _thirdStack = new Stack("3", 12, 42, 22, 0);
            Stack _fourthStack = new Stack("4", 26, 36, 20, 0);
            Stack _fifthStack = new Stack("5", 24, 0, 0, 0);

            Globals.Stacks.Add(_firstStack);
            Globals.Stacks.Add(_secondStack);
            Globals.Stacks.Add(_thirdStack);
            Globals.Stacks.Add(_fourthStack);
            Globals.Stacks.Add(_fifthStack);
            
            Stacks.Add(_firstStack);
            Stacks.Add(_secondStack);
            Stacks.Add(_thirdStack);
            Stacks.Add(_fourthStack);
            Stacks.Add(_fifthStack);
        }

        private void InitColony(Colony colony, WorkerRank firstWorker, WorkerRank secondWorker, WorkerRank thirdWorker, WarriorRank firstWarrior, WarriorRank secondWarrior, WarriorRank thirdWarrior)
        {
            colony.Workers.Add(new AntWorker(2, 1, 0, colony.Queen, firstWorker));
            colony.Workers.Add(new AntWorker(8, 4, 0, colony.Queen, secondWorker));
            colony.Workers.Add(new AntWorker(10, 6, 0, colony.Queen, thirdWorker));

            colony.Warriors.Add(new AntWarrior(0, 0, 0, colony.Queen, firstWarrior));
            colony.Warriors.Add(new AntWarrior(0, 0, 0, colony.Queen, secondWarrior));
            colony.Warriors.Add(new AntWarrior(0, 0, 0, colony.Queen, thirdWarrior));
        }
        #endregion
    }
}