namespace Advent
{
    public class Day09A : Day09
    {
        protected override int RunCore(Input input)
        {
            return new Groups(input).Score();
        }
    }
}