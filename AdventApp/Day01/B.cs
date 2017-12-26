namespace Advent.Day01
{
    public sealed class B : Base
    {
        protected override int RunCore(Input input) => Captcha.Value(input, input.Length / 2);
    }
}