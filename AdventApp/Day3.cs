namespace Advent
{
    public abstract class Day3 : ICanRun
    {
        public string DefaultInput
        {
            get => @"265149";
        }

        public int Run(string input)
        {
            return this.RunCore(input);
        }

        protected abstract int RunCore(string input);
    }
}