namespace Advent.Day07
{
    public class A : Base
    {
        protected override string RunCore(Input input)
        {
            return new ProgramTree(input).Root.Name;
        }
    }
}