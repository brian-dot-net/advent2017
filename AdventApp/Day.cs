namespace Advent
{
    using System.Diagnostics;
    using System.IO;

    public static class Day<TDay> where TDay : ICanRun, new()
    {
        public static void Show(TextWriter w)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            string result = Run();
            stopwatch.Stop();
            w.WriteLine("{0} => {1} ({2} ms elapsed)", typeof(TDay).Name, result, stopwatch.ElapsedMilliseconds);
        }

        public static string Run()
        {
            TDay day = new TDay();
            string input = day.DefaultInput;
            return day.Run(input);
        }
    }
}