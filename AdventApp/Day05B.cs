namespace Advent
{
    using System.Linq;

    public class Day05B : Day05
    {
        protected override int RunCore(Input input) => new JumpTable(input, o => (o > 2) ? -1 : 1).Count();
    }
}