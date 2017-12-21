namespace Advent
{
    public class Day7A : Day7
    {
        protected override string RunCore(string input)
        {
            return new ProgramTree(input).Root.Name;
        }
    }
}