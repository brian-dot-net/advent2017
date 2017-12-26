namespace Advent.Day10
{
    public class A : Base
    {
        protected override string RunCore(Input input)
        {
            byte[] result = HashBytes(input);
            return (result[0] * result[1]).ToString();
        }
    }
}