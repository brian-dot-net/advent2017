namespace Advent
{
    using System.Linq;

    public class Day05A : Day05
    {
        protected override int RunCore(string input) => new JumpTable(input, o => 1).Count();
    }
}