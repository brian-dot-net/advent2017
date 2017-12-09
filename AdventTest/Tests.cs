﻿namespace Advent.Test
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class Tests
    {
        [TestMethod]
        public void TestShow()
        {
            StringBuilder output = new StringBuilder();
            using (TextWriter w = new StringWriter(output))
            {
                Day.Show<ADay>(w);
            }

            output.ToString().Should().Be("ADay => -1\r\n");
        }

        [TestMethod]
        public void TestDay1A()
        {
            Try<Day1A>(
                P("1122", 3),
                P("1111", 4),
                P("1234", 0),
                P("91212129", 9));
        }

        [TestMethod]
        public void TestDay1B()
        {
            Try<Day1B>(
                P("1212", 6),
                P("1221", 0),
                P("123425", 4),
                P("123123", 12),
                P("12131415", 4));
        }

        [TestMethod]
        public void TestDay2A()
        {
            string i1 = @"5 1 9 5
7 5 3
2 4 6 8";
            Try<Day2A>(P(i1, 18));
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

        private sealed class ADay : ICanRun
        {
            public string DefaultInput => "-1";

            public int Run(string input)
            {
                return int.Parse(input);
            }
        }
    }
}
