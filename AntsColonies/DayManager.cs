using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal class DayManager
    {
        internal List<Colony> Colonies = new List<Colony>();
        internal List<Stack> Stacks = new List<Stack>();
        
        internal Hike hike = new Hike();

        protected internal void Day()
        {
            hike.Start(Colonies, Stacks);

            foreach (Stack stack in Stacks)
            {
                stack.PrintStack();
                foreach (Squad squad in stack.CurrentSquads)
                {
                    Console.WriteLine($"{squad.SquadName}: воин: {squad.StackWarriors.Count}; рабочий: {squad.StackWorkers.Count}");
                }
            }
            
            hike.StartBattle(Stacks);

            foreach (Colony colony in Colonies)
            {
                Console.WriteLine("Воинов назад: " + colony.Warriors.Count);
                Console.WriteLine("Рабочих назад: " + colony.Workers.Count);
            }
        }

        #region Initialization
        protected internal void InitGame()
        {
            InitStacks();
            
            // init first colony (red)
            Colony _redColony = new Colony(new Queen("Изабелла", 23, 8, 26),"Красные", 18, 9, 1);
            InitColony(_redColony, CreatureRank.Elder, CreatureRank.Elite, CreatureRank.Legend, CreatureRank.Legend, CreatureRank.Elite, CreatureRank.Elite);
            _redColony.PrintColony();

            Globals.Colonies.Add(_redColony);
            
            // init second colony (orange)
            Colony _orangeColony = new Colony(new Queen("Клеопатра", 21, 6, 21),"Рыжие", 18, 6, 1);
            InitColony(_orangeColony, CreatureRank.Elder, CreatureRank.Legend, CreatureRank.Advanced, CreatureRank.Elder, CreatureRank.Elder, CreatureRank.Advanced);
            _orangeColony.PrintColony();
            
            Globals.Colonies.Add(_orangeColony);
            
            Colonies.Add(_redColony);
            Colonies.Add(_orangeColony);
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

        private void InitColony(Colony colony, CreatureRank firstWorker, CreatureRank secondWorker, CreatureRank thirdWorker, CreatureRank firstWarrior, CreatureRank secondWarrior, CreatureRank thirdWarrior)
        {
            colony.Workers.Add(new AntWorker(2, 1, 0, colony.Queen.Name, firstWorker));
            colony.Workers.Add(new AntWorker(8, 4, 0, colony.Queen.Name, secondWorker));
            colony.Workers.Add(new AntWorker(10, 6, 0, colony.Queen.Name, thirdWorker));

            colony.Warriors.Add(new AntWarrior(0, 0, 0, colony.Queen.Name, firstWarrior));
            colony.Warriors.Add(new AntWarrior(0, 0, 0, colony.Queen.Name, secondWarrior));
            colony.Warriors.Add(new AntWarrior(0, 0, 0, colony.Queen.Name, thirdWarrior));
        }
        #endregion
    }
}