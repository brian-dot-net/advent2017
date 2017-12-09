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
        }
    }
}
