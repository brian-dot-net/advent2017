namespace Advent
{
    public class Day12B : Day12
    {
        protected override int RunCore(Input input) => new Pipes(input).CountGroups();
    }
}