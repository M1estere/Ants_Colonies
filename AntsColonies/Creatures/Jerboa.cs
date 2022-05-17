namespace AntsColonies
{
    public class Jerboa
    {
        internal readonly int FirstDay;

        internal readonly int RandomDay;
        
        internal Jerboa(int allDays)
        {
            FirstDay = Globals.Random.Next(1, allDays - 7); // день начала
            int lastDay = FirstDay + 8;
            
            RandomDay = Globals.Random.Next(FirstDay, lastDay + 1); // когда нападет тушканчик
        }

        internal static void Action()
        {
            foreach (var colony in Globals.Colonies)
            {
                colony.Workers.Clear();
            }
        }
    }
}