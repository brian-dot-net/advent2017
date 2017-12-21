namespace Advent
{
    public class Day8A : Day8
    {
        protected override int RunCore(string input)
        {
            return new Instructions(input).Run();
        }
    }
}