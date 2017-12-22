namespace Advent
{
    using System;
    using System.Linq;
    using System.Text;

    public class Day10B : Day10
    {
        protected override string RunCore(Input input)
        {
            byte[] bytes = input.AsciiBytes().Concat(new byte[] { 17, 31, 73, 47, 23 }).ToArray();
            byte[] result = new Knot(256).Hash(64, bytes);
            result = Compact(result);
            return BitConverter.ToString(result).Replace("-", string.Empty).ToLowerInvariant();
        }

        private static byte[] Compact(byte[] input)
        {
            byte[] result = new byte[16];
            for (int i = 0; i < 16; ++i)
            {
                result[i] = Hash(input, i * 16);
            }

            return result;
        }

        private static byte Hash(byte[] input, int start)
        {
            byte result = input[start];
            for (int i = 1; i < 16; ++i)
            {
                result ^= input[start + i];
            }

            return result;
        }
    }
}