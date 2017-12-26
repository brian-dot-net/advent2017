namespace Advent.Day08
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new Instructions(input).Run().MaxValueEver;
    }
}