namespace Advent
{
    using System.Linq;

    public class Day10A : Day10
    {
        protected override string RunCore(Input input)
        {
            string[] fields = input.Raw.Split(':');
            if (fields.Length == 1)
            {
                fields = new string[] { "256", fields[0] };
            }

            int length = int.Parse(fields[0]);
            byte[] bytes = fields[1].Split(',').Select(byte.Parse).ToArray();
            byte[] result = new Knot(length).Hash(1, bytes);
            return (result[0] * result[1]).ToString();
        }
    }
}