namespace Advent
{
    using System;

    internal sealed class Program
    {
        private static void Main()
        {
            Show<Day01.A>();
            Show<Day01.B>();
            Show<Day02.A>();
            Show<Day02.B>();
            Show<Day03.A>();
            Show<Day03.B>();
            Show<Day04.A>();
            Show<Day04.B>();
            Show<Day05.A>();
            Show<Day05.B>();
            Show<Day06.A>();
            Show<Day06.B>();
            Show<Day07.A>();
            Show<Day07.B>();
            Show<Day08.A>();
            Show<Day08.B>();
            Show<Day09.A>();
            Show<Day09.B>();
            Show<Day10.A>();
            Show<Day10.B>();
            Show<Day11.A>();
            Show<Day11.B>();
            Show<Day12.A>();
            Show<Day12.B>();
            Show<Day13.A>();
            Show<Day13.B>();
            Show<Day14.A>();
            Show<Day14.B>();
            Show<Day15.A>();
            Show<Day15.B>();
            Show<Day16.A>();
            Show<Day16.B>();
            Show<Day17.A>();
            Show<Day17.B>();
            Show<Day18.A>();
            Show<Day18.B>();
        }

        private static void Show<TDay>() where TDay : ICanRun, new() => Day<TDay>.Show(Console.Out);
    }
}
