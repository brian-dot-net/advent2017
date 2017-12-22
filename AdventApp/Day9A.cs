namespace Advent
{
    public class Day9A : Day9
    {
        protected override int RunCore(string input)
        {
            return new Groups(input).Score();
        }
    }
}