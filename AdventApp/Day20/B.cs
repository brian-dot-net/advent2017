namespace Advent.Day20
{
    public class B : Base
    {
        protected override int RunCore(Input input) => new Particles(input).RunCollisions();
    }
}