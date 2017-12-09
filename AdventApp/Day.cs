namespace Advent
{
    using System;
    using System.IO;

    public static class Day
    {
        public static void Show<TDay>(TextWriter w) where TDay : ICanRun, new()
        {
            TDay day = new TDay();
            w.WriteLine("{0} => {1}", typeof(TDay).Name, day.Run(day.DefaultInput));
        }
    }
}