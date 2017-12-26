namespace Advent.Day12
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new Pipes(input).CountGroups();
    }
}