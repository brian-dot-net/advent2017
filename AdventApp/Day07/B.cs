namespace Advent.Day07
{
    public class B : Base
    {
        protected override string RunCore(Input input)
        {
            return new ProgramTree(input).Root.GetImbalance();
        }
    }
}