namespace Advent
{
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

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
            if (input == null)
            {
                Assembly asm = typeof(TDay).Assembly;
                string baseName = typeof(TDay).Name;
                baseName = baseName.Substring(0, baseName.Length - 1);
                string inputResource = typeof(TDay).Namespace + "." + baseName + ".txt";
                using (StreamReader reader = new StreamReader(asm.GetManifestResourceStream(inputResource)))
                {
                    input = reader.ReadToEnd();
                }
            }

            return day.Run(input);
        }
    }
}