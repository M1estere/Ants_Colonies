using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal static class Globals
    {
        internal static Random Random = new Random();

        internal static List<Colony> Colonies = new List<Colony>();

        internal static List<Stack> Stacks = new List<Stack>();
    }
}