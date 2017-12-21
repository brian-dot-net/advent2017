namespace Advent
{
    using System;
    using System.IO;

    internal sealed class Program
    {
        private static void Main()
        {
            TextWriter o = Console.Out;
            Day.Show<Day1A>(o);
            Day.Show<Day1B>(o);
            Day.Show<Day2A>(o);
            Day.Show<Day2B>(o);
            Day.Show<Day3A>(o);
            Day.Show<Day3B>(o);
            Day.Show<Day4A>(o);
            Day.Show<Day4B>(o);
            Day.Show<Day5A>(o);
            Day.Show<Day5B>(o);
            Day.Show<Day6A>(o);
            Day.Show<Day6B>(o);
            Day.Show<Day7A>(o);
            Day.Show<Day7B>(o);
        }
    }
}
