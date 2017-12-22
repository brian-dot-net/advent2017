namespace Advent
{
    public class Day07B : Day07
    {
        protected override string RunCore(string input)
        {
            return new ProgramTree(input).Root.GetImbalance();
        }
    }
}