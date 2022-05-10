using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal class Stack
    {
        internal string Name;

        internal List<Squad> CurrentSquads = new List<Squad>();

        internal int Branches;
        internal int Leaves;
        internal int Dewdrops;
        internal int Stones;

        public Stack(string name, int branches, int leaves, int dewdrops, int stones)
        {
            Name = name;
            Branches = branches;
            Leaves = leaves;
            Dewdrops = dewdrops;
            Stones = stones;
        }

        public void PrintStack()
        {
            if (Branches == 0 && Leaves == 0 && Dewdrops == 0 && Stones == 0)
                Console.WriteLine($"Куча {Name}: Истощена");
            else
               Console.WriteLine($"Куча {Name}: Веток: {Branches}; Листьев: {Leaves}; Росинок: {Dewdrops}; Камней: {Stones}");
        }
    }
}