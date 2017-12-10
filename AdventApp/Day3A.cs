namespace Advent
{
    public sealed class Day3A : Day3
    {
        protected override int RunCore(string input)
        {
            int n = int.Parse(input);
            Steps steps = new Steps(n);
            while (steps.MoveMany())
            {
            }

            return steps.Distance;
        }
    }
}