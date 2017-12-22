namespace Advent
{
    using System.Linq;

    public class Day10A : Day10
    {
        protected override string RunCore(Input input)
        {
            Input[] fields = input.Fields();
            if (fields.Length == 1)
            {
                fields = new Input[] { new Input("256"), fields[0] };
            }

            int length = fields[0].Integer();
            byte[] bytes = fields[1].Fields(',').Select(f => f.Byte()).ToArray();
            byte[] result = new Knot(length).Hash(1, bytes);
            return (result[0] * result[1]).ToString();
        }
    }
}