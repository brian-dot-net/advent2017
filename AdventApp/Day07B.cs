namespace Advent
{
    public class Day07B : Day07
    {
        protected override string RunCore(Input input)
        {
            return new ProgramTree(input).Root.GetImbalance();
        }
    }
}