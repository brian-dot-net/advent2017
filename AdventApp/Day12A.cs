namespace Advent
{
    public class Day12A : Day12
    {
        protected override int RunCore(Input input) => new Pipes(input).Count(0);
    }
}