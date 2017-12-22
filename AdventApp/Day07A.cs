namespace Advent
{
    public class Day07A : Day07
    {
        protected override string RunCore(Input input)
        {
            return new ProgramTree(input).Root.Name;
        }
    }
}