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
        
        internal int GrowthCycle;

        internal LarvaType Type;

        internal Queen Queen;
        
        internal Larva(int growthCycle, LarvaType type, Queen queen) : base()
        {
            GrowthCycle = growthCycle;
            Type = type;
            Queen = queen;
        }

        internal void DecreaseGrowthDays()
        {
            if (GrowthCycle > 0)
            {
                GrowthCycle--;
            } 
            if (GrowthCycle <= 0)
            {
                CreateAnt();
            }
        }
        
        private void CreateAnt()
        {
            switch (Type)
            {
                case LarvaType.Worker:
                    Array valuesWorker = WorkerRank.GetValues(typeof(WorkerRank));
                    WorkerRank randomWorkerRank = (WorkerRank)valuesWorker.GetValue(Globals.Random.Next(valuesWorker.Length));
                    
                    Queen.Colony.Workers.Add(new AntWorker(0, 0, 0, Queen, randomWorkerRank));
                    //Console.WriteLine($"В колонии {Queen.Colony.Name} появился рабочий {randomWorkerRank}!");
                    Queen.Colony.Larvas.Remove(this);

                    Queen.Colony.NewWorkers++;
                    
                    break;
                case LarvaType.Warrior:
                    Array valuesWarrior = WarriorRank.GetValues(typeof(WarriorRank));
                    WarriorRank randomWarriorRank = (WarriorRank)valuesWarrior.GetValue(Globals.Random.Next(valuesWarrior.Length));
                    
                    Queen.Colony.Warriors.Add(new AntWarrior(0, 0, 0, Queen, randomWarriorRank));
                    //Console.WriteLine($"В колонии {Queen.Colony.Name} появился воин {randomWarriorRank}!");
                    Queen.Colony.Larvas.Remove(this);

                    Queen.Colony.NewWarriors++;
                    
                    break;
                case LarvaType.Queen:
                    string name = Globals.QueenNames[Globals.Random.Next(0, Globals.QueenNames.Count)];
                    Queen queen = new Queen(name, Queen.Health, Queen.Protection, Queen.Damage, 
                        4, 6, 3, 4, false);
                    
                    Globals.QueenNames.Remove(name);
                    
                    Queen.Colony.Larvas.Remove(this);
                    
                    HandleQueen(queen);
                    break;
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
            Console.WriteLine($"Королева {queen.Name} создает колонию {colonyName} родственную к {Queen.Colony.Name}");
            Console.WriteLine();
            Colony colony = new Colony(colonyName, 0, 0);
            
            colony.Queen = queen;
            queen.Colony = colony;

            queen.FriendQueens.Add(Queen);
            Queen.FriendQueens.Add(queen);
            
            Globals.Colonies.Add(colony);
            Globals.ColoniesNames.Remove(colonyName);
        }
    }
}