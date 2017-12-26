namespace Advent.Day07
{
    public class A : Base
    {
        protected override string RunCore(Input input) => new ProgramTree(input).Root.Name;
    }
}