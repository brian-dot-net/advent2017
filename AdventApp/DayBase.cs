namespace Advent
{
    public abstract class DayBase<TResult> : ICanRun
    {
        public abstract string DefaultInput { get; }

        public string Run(string input)
        {
            return this.RunCore(input).ToString();
        }

        protected abstract TResult RunCore(string input);
    }
}