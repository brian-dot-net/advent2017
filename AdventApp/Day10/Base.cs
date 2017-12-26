namespace Advent.Day10
{
    using System;
    using System.Linq;

    public abstract class Base : DayBase<string>
    {
        protected static byte[] HashBytes(Input input)
        {
            Input[] fields = input.Fields();
            if (fields.Length == 1)
            {
                fields = new Input[] { new Input("256"), fields[0] };
            }

            int length = fields[0].Integer();
            byte[] bytes = fields[1].Fields(",").Select(f => f.Byte()).ToArray();
            return new Knot(length).Hash(1, bytes);
        }

        protected static string HexValue(byte[] result) => BitConverter.ToString(result).Replace("-", string.Empty).ToLowerInvariant();
    }
}