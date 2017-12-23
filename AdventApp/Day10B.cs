namespace Advent
{
    using System;

    public class Day10B : Day10
    {
        protected override string RunCore(Input input)
        {
            byte[] result = Knot.Hash(input);
            return BitConverter.ToString(result).Replace("-", string.Empty).ToLowerInvariant();
        }
    }
}