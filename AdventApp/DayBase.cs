namespace Advent
{
    using System;
    using System.IO;
    using System.Reflection;

    public abstract class DayBase<TResult> : ICanRun
    {
        public string DefaultInput
        {
            get
            {
                Type type = this.GetType();
                Assembly asm = type.Assembly;
                string baseName = type.Name;
                baseName = baseName.Substring(0, baseName.Length - 1);
                string inputResource = type.Namespace + "." + baseName + ".txt";
                using (StreamReader reader = new StreamReader(asm.GetManifestResourceStream(inputResource)))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public string Run(string input)
        {
            return this.RunCore(new Input(input)).ToString();
        }

        protected abstract TResult RunCore(Input input);
    }
}