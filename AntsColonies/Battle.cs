using System;

namespace AntsColonies
{
    internal class Battle
    {

        private Stack _stack;
        
        private Squad _firstSquad;
        private Squad _secondSquad;
        
        public Battle(Stack stack, Squad first, Squad second)
        {
            _stack = stack;
            _firstSquad = first;
            _secondSquad = second;

            Start();
        }

        private void Start()
        {
            /*foreach (AntWarrior warrior in _firstSquad.StackWarriors)
            {
                int randomFirst = Globals.Random.Next(0, 2);

                int randomSecond;
                
                if (randomFirst % 2 == 0)
                {
                    randomSecond = Globals.Random.Next(0, _secondSquad.StackWarriors.Count);
                    warrior.Attack(_secondSquad.StackWarriors[randomSecond]);
                } else
                {
                    randomSecond = Globals.Random.Next(0, _secondSquad.StackWorkers.Count);
                    warrior.Attack(_secondSquad.StackWorkers[randomSecond]);
                }
            }

            foreach (AntWarrior warrior in _secondSquad.StackWarriors)
            {
                int randomFirst = Globals.Random.Next(0, 2);

                int randomSecond;
                
                if (randomFirst % 2 == 0)
                {
                    randomSecond = Globals.Random.Next(0, _firstSquad.StackWarriors.Count);
                    warrior.Attack(_firstSquad.StackWarriors[randomSecond]);
                } else
                {
                    randomSecond = Globals.Random.Next(0, _firstSquad.StackWorkers.Count);
                    warrior.Attack(_firstSquad.StackWorkers[randomSecond]);
                }
            }
            
            FinishBattle();*/
        }
    }
}