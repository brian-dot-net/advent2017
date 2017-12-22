namespace Advent
{
    using System;

    internal sealed class Program
    {
        private static void Main()
        {
            Show<Day01A>();
            Show<Day01B>();
            Show<Day02A>();
            Show<Day02B>();
            Show<Day03A>();
            Show<Day03B>();
            Show<Day04A>();
            Show<Day04B>();
            Show<Day05A>();
            Show<Day05B>();
            Show<Day06A>();
            Show<Day06B>();
            Show<Day07A>();
            Show<Day07B>();
            Show<Day08A>();
            Show<Day08B>();
            Show<Day09A>();
            Show<Day09B>();
            Show<Day10A>();
            Show<Day10B>();
        }

        private static void Show<TDay>() where TDay : ICanRun, new() => Day<TDay>.Show(Console.Out);
    }
}
