namespace Advent.Day10
{
    using System;

    public class B : Base
    {
        protected override string RunCore(Input input)
        {
            byte[] result = Knot.Hash(input);
            return BitConverter.ToString(result).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}