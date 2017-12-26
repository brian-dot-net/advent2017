namespace Advent.Day08
{
    public class A : Base
    {
        protected override int RunCore(Input input)
        {
            return new Instructions(input).Run().MaxValue();
        }
    }
}