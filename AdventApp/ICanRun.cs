namespace Advent
{
    public interface ICanRun
    {
        string DefaultInput { get; }

        int Run(string input);
    }
}