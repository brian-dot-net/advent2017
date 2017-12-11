namespace Advent
{
    public sealed class Day3A : Day3
    {
        protected override int RunCore(string input)
        {
            int n = int.Parse(input);
            Ring ring = new Ring(0);
            while (ring.Max < n)
            {
                ring = ring.Next();
            }

            return ring.Distance(n);
        }
    }
}