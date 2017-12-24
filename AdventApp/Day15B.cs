namespace Advent
{
    public class Day15B : Day15
    {
        protected override int RunCore(Input input) => new Generators(input).Run(5000000, 4, 8);
    }
}