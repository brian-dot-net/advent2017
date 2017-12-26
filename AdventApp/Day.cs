namespace Advent
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    public static class Day<TDay> where TDay : ICanRun, new()
    {
        private static readonly string Name = GetName(typeof(TDay));

        public static void Show(TextWriter w)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            string result = Run();
            stopwatch.Stop();
            w.WriteLine("{0} => {1} ({2} ms elapsed)", Name, result, stopwatch.ElapsedMilliseconds);
        }

        public static string Run()
        {
            TDay day = new TDay();
            string input = day.DefaultInput;
            return day.Run(input);
        }

        private static string GetName(Type type) => type.Namespace.Split('.').Last() + "." + type.Name;
    }
}