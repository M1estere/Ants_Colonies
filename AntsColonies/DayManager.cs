using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal class DayManager
    {
        internal List<Stack> Stacks = new List<Stack>();
        
        internal Hike hike = new Hike();

        internal DayManager()
        {
            InitGame();
        }
        
        protected internal void Day()
        {
            foreach (Colony colony in Globals.Colonies)
            {
                if (colony.Larvas.Count == 0)
                    colony.Queen.MakeLarvas(); // creating larvas
            }
            
            hike.Start(Globals.Colonies, Stacks);

            foreach (Stack stack in Stacks)
            {
                stack.PrintStack();
                foreach (Squad squad in stack.CurrentSquads)
                {
                    Console.WriteLine($"{squad.SquadName}: воин: {squad.StackWarriors.Count}; рабочий: {squad.StackWorkers.Count}; особенных: {squad.StackSpecials.Count}");
                }
            }
            
            hike.StartBattle(Stacks);

            foreach (Colony colony in Globals.Colonies)
            {
                Console.WriteLine();
                Console.WriteLine($"Колония: {colony.Name}");
                Console.WriteLine("Воинов назад: " + colony.Warriors.Count);
                Console.WriteLine("Рабочих назад: " + colony.Workers.Count);
                Console.WriteLine("Особенных назад: " + colony.Specials.Count);
                Console.WriteLine();
                colony.PrintColony();
            }

            for (int j = 0; j < Globals.Colonies.Count; j++)
            {
                for (int i = 0; i < Globals.Colonies[j].Larvas.Count; i++)
                {
                    Globals.Colonies[j].Larvas[i].DecreaseGrowthDays();
                }
            }
        }

        #region Initialization
        protected internal void InitGame()
        {
            InitStacks();
            
            // init first colony (red)
<<<<<<< HEAD
            Colony _redColony = new Colony("Красные", 18, 9);
            Queen Isabella = new Queen("Изабелла", 23, 8, 26, 3, 4, 3, 4, true, null, _redColony);
=======
            Colony _redColony = new Colony(new Queen("Изабелла", 23, 8, 26, 0, 0),"Красные", 18, 9, 1);
            InitColony(_redColony, CreatureRank.Elder, CreatureRank.Elite, CreatureRank.Legend, CreatureRank.Legend, CreatureRank.Elite, CreatureRank.Elite);
            _redColony.PrintColony();
>>>>>>> 45647807db5c2f8efec21abc70a700949550b749

            _redColony.Queen = Isabella;

            _redColony.Specials.Add(new SpecialInsect("Оса", SpecialInsectRank.Hardworker, Isabella, 0, 0, 0));
            
            InitColony(_redColony, WorkerRank.ElderOne, WorkerRank.Elite, WorkerRank.Legend, WarriorRank.Legend, WarriorRank.Elite, WarriorRank.Elite);
            _redColony.PrintColony();
            
            // init second colony (orange)
<<<<<<< HEAD
            Colony _orangeColony = new Colony("Рыжие", 18, 6);
            Queen Kleopatra = new Queen("Клеопатра", 21, 6, 21, 1, 4, 3, 5, true, null, _orangeColony);

            _orangeColony.Queen = Kleopatra;

            _orangeColony.Specials.Add(new SpecialInsect("Толстоножка", SpecialInsectRank.Lazy, Kleopatra, 0, 0, 0));
            
            InitColony(_orangeColony, WorkerRank.ElderTwo, WorkerRank.Legend, WorkerRank.AdvancedForget, WarriorRank.Elder, WarriorRank.Elder, WarriorRank.Advanced);
=======
            Colony _orangeColony = new Colony(new Queen("Клеопатра", 21, 6, 21, 0, 0),"Рыжие", 18, 6, 1);
            InitColony(_orangeColony, CreatureRank.Elder, CreatureRank.Legend, CreatureRank.Advanced, CreatureRank.Elder, CreatureRank.Elder, CreatureRank.Advanced);
>>>>>>> 45647807db5c2f8efec21abc70a700949550b749
            _orangeColony.PrintColony();
            
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