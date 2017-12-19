namespace Advent
{
    using System.Diagnostics;
    using System.IO;

    public static class Day
    {
        public static void Show<TDay>(TextWriter w) where TDay : ICanRun, new()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            TDay day = new TDay();
            int result = day.Run(day.DefaultInput);
            stopwatch.Stop();
            w.WriteLine("{0} => {1} ({2} ms elapsed)", typeof(TDay).Name, result, stopwatch.ElapsedMilliseconds);
        }
    }
}