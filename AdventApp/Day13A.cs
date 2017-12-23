namespace Advent
{
    public class Day13A : Day13
    {
        protected override int RunCore(Input input) => new Firewall(input).Severity();
    }
}