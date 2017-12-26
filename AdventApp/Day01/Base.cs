namespace Advent.Day01
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Base : DayBase<int>
    {
        protected static class Captcha
        {
            public static int Value(Input input, int r)
            {
                return input.Chars().Zip(Rotate(input.Chars(), r), Matching).Sum();
            }

            private static int Matching(char a, char b) => (a == b) ? (a - '0') : 0;

            private static IEnumerable<T> Rotate<T>(IEnumerable<T> input, int n) => input.Skip(n).Concat(input.Take(n));
        }
    }
}