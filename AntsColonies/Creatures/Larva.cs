using System;

namespace AntsColonies
{
    internal class Larva : Creature
    {
        internal enum LarvaType
        {
            Worker,
            Warrior,
            Queen,
        }

        private int _growthCycle;

        private readonly LarvaType _type;

        private readonly Queen _queen;
        
        internal Larva(int growthCycle, LarvaType type, Queen queen)
        {
            _growthCycle = growthCycle;
            _type = type;
            _queen = queen;
        }

        internal void DecreaseGrowthDays()
        {
            if (_growthCycle > 0)
            {
                _growthCycle--;
            } 
            if (_growthCycle <= 0)
            {
                CreateAnt();
            }
        }
        
        private void CreateAnt()
        {
            switch (_type)
            {
                case LarvaType.Worker:
                    Array valuesWorker = WorkerRank.GetValues(typeof(WorkerRank));
                    WorkerRank randomWorkerRank = (WorkerRank)valuesWorker.GetValue(Globals.Random.Next(valuesWorker.Length));
                    
                    _queen.Colony.Workers.Add(new AntWorker(0, 0, 0, _queen, randomWorkerRank));
                    _queen.Colony.Larvas.Remove(this);

                    _queen.Colony.NewWorkers++;
                    
                    break;
                case LarvaType.Warrior:
                    Array valuesWarrior = WarriorRank.GetValues(typeof(WarriorRank));
                    WarriorRank randomWarriorRank = (WarriorRank)valuesWarrior.GetValue(Globals.Random.Next(valuesWarrior.Length));
                    
                    _queen.Colony.Warriors.Add(new AntWarrior(0, 0, 0, _queen, randomWarriorRank));
                    _queen.Colony.Larvas.Remove(this);

                    _queen.Colony.NewWarriors++;
                    
                    break;
                case LarvaType.Queen:
                    string name = Globals.QueenNames[Globals.Random.Next(0, Globals.QueenNames.Count)];
                    Queen queen = new Queen(name, _queen.Health, _queen.Protection, _queen.Damage, 
                        4, 6, 3, 4, false);
                    
                    Globals.QueenNames.Remove(name);
                    
                    _queen.Colony.Larvas.Remove(this);
                    
                    HandleQueen(queen);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleQueen(Queen queen)
        {
            int randomAction = Globals.Random.Next(0, 2);
            if (randomAction % 2 == 0)
            {
                // удалять королеву
                Console.WriteLine($"Новая королева {queen.Name} затерялась!");
                Console.WriteLine();
                Globals.QueenNames.Add(queen.Name);
            } else
            {
                // королева создает новую колонию
                CreateColony(queen);
            }
        }

        private void CreateColony(Queen queen)
        {
            string colonyName = Globals.ColoniesNames[Globals.Random.Next(0, Globals.ColoniesNames.Count)];
            Console.WriteLine($"Королева {queen.Name} создает колонию {colonyName} родственную к {_queen.Colony.Name}");
            Console.WriteLine();
            Colony colony = new Colony(colonyName, 0, 0);
            
            colony.Queen = queen;
            queen.Colony = colony;

            queen.FriendQueens.Add(_queen);
            _queen.FriendQueens.Add(queen);
            
            Globals.Colonies.Add(colony);
            Globals.ColoniesNames.Remove(colonyName);
        }
    }
}