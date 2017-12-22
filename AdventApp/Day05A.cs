namespace Advent
{
    using System.Linq;

    public class Day05A : Day05
    {
        protected override int RunCore(Input input) => new JumpTable(input, o => 1).Count();
    }
}