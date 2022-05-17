using System;
using System.Collections.Generic;

namespace AntsColonies
{
    internal static class Globals
    {
        internal static Random Random = new Random();

        internal static List<Colony> Colonies = new();

        internal static List<Stack> Stacks = new();

        internal static List<string> QueenNames = new() { "Мария", "Роза", "Сина", "Елизавета", "Констанция", "Клавдия", "Анна", "Екатерина", "Беатриса" };

        internal static List<string> ColoniesNames = new() { "Голубые", "Черные", "Желтые", "Фиолетовые", "Зеленые", "Синие", "Коричневые", "Серые", "Белые" };
    }
}