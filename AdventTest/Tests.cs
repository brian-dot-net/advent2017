namespace Advent.Test
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class Tests
    {
        [TestMethod]
        public void TestDay1A()
        {
            Try<Day1A>(
                P("1122", 3),
                P("1111", 4),
                P("1234", 0),
                P("91212129", 9));
        }

        private static KeyValuePair<TKey, TValue> P<TKey, TValue>(TKey key, TValue value)
        {
            return new KeyValuePair<TKey, TValue>(key, value);
        }

        private static void Try<TDay>(params KeyValuePair<string, int>[] pairs) where TDay : ICanRun, new()
        {
            foreach (KeyValuePair<string, int> pair in pairs)
            {
                string input = pair.Key;
                new TDay().Run(pair.Key).Should().Be(pair.Value, "{0} was the input", input);
            }
        }
    }
}
