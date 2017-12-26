namespace Advent.Day08
{
    public class B : Base
    {
        protected override int RunCore(Input input)
        {
            return new Instructions(input).Run().MaxValueEver;
        }
    }
}