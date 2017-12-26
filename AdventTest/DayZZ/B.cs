namespace Advent.Test.DayZZ
{
    internal sealed class B : DayBase<int>
    {
        protected override int RunCore(Input input) => input.Integer();
    }
}