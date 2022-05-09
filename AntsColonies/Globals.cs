using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal static class Globals
    {
        internal static Random Random = new Random();

        internal static List<Colony> Colonies = new List<Colony>();

        internal static List<Stack> Stacks = new List<Stack>();

        internal static List<string> QueenNames = new List<string>() { "Мария", "Роза", "Сина", "Елизавета", "Констанция", "Клавдия", "Анна", "Екатерина", "Беатриса" };

        internal static List<string> ColoniesNames = new List<string>() { "Голубые", "Черные", "Желтые", "Фиолетовые", "Зеленые", "Синие", "Коричневые", "Серые", "Белые" };
    }
}