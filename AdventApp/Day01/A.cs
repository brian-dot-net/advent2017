namespace Advent.Day01
{
    public sealed class A : Base
    {
        protected override int RunCore(Input input) => Captcha.Value(input, 1);
    }
}