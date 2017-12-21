namespace Advent
{
    public interface ICanRun
    {
        string DefaultInput { get; }

        string Run(string input);
    }
}