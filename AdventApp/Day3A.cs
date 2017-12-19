namespace Advent
{
    public sealed class Day3A : Day3
    {
        protected override int RunCore(string input)
        {
            int n = int.Parse(input);
            Ring ring = Ring.First();
            while (ring.Max < n)
            {
                ring = ring.Next();
            }

            return ring.Distance(n);
        }
    }
}