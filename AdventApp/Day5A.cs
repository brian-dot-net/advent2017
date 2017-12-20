namespace Advent
{
    using System.Linq;

    public class Day5A : Day5
    {
        protected override int RunCore(string input) => new JumpTable(input, o => 1).Count();
    }
}