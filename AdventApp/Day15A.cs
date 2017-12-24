namespace Advent
{
    public class Day15A : Day15
    {
        protected override int RunCore(Input input) => new Generators(input).Run(40000000, 1, 1);
    }
}