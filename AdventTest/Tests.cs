﻿namespace Advent.Test
{
    using System;
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

            output.ToString().Should().Match("ADay => -1 (* ms elapsed)\r\n");
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

        [TestMethod]
        public void TestDay2B()
        {
            string i1 = @"5 9 2 8
9 4 7 3
3 8 6 5";
            Try<Day2B>(P(i1, 9));
        }

        [TestMethod]
        public void TestDay3A()
        {
            Try<Day3A>(
                P("1", 0),
                P("12", 3),
                P("23", 2),
                P("1024", 31));
        }

        [TestMethod]
        public void TestDay3B()
        {
            Try<Day3B>(
                P("1", 2),
                P("12", 23),
                P("23", 25),
                P("747", 806));
        }

        [TestMethod]
        public void TestDay4A()
        {
            string many = @"a b c d
e f g h
a a b c
a b c c";
            Try<Day4A>(
                P("aa bb cc dd ee", 1),
                P("aa bb cc dd aa", 0),
                P("aa bb cc dd aaa", 1),
                P(many, 2));
        }

        [TestMethod]
        public void TestDay4B()
        {
            string many = @"a b c d
e f g h
a a b c
a b ca ac";
            Try<Day4B>(
                P("abcde fghij", 1),
                P("abcde xyz ecdab", 0),
                P("a ab abc abd abf abj", 1),
                P("iiii oiii ooii oooi oooo", 1),
                P("oiii ioii iioi iiio", 0),
                P(many, 2));
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
                int result = -1;
                Action act = () => result = new TDay().Run(pair.Key);

                act.ShouldNotThrow("{0} was the input", input);
                result.Should().Be(pair.Value, "{0} was the input", input);
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
