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
                string fileName = type.Name;
                fileName = fileName.Substring(0, fileName.Length - 1);
                if (fileName.Length == 0)
                {
                    fileName = "Input";
                }

                fileName += ".txt";

                string inputResource = type.Namespace + "." + fileName;
                Stream stream = asm.GetManifestResourceStream(inputResource);
                if (stream == null)
                {
                    throw new FileNotFoundException("Could not find '" + fileName + "'.");
                }

                using (StreamReader reader = new StreamReader(stream))
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