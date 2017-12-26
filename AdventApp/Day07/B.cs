namespace Advent.Day07
{
    public class B : Base
    {
        protected override string RunCore(Input input) => new ProgramTree(input).Root.GetImbalance();
    }
}