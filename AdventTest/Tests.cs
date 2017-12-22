namespace Advent.Test
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
                Day<ADay>.Show(w);
                Day<BDay>.Show(w);
            }

            output.ToString().Should().Match(
                "ADay => x-1 (* ms elapsed)\r\n" +
                "BDay => 500 (* ms elapsed)\r\n");
        }

        [TestMethod]
        public void Day01ATests()
        {
            Do<Day01A>.Tests(
                P("1122", 3),
                P("1111", 4),
                P("1234", 0),
                P("91212129", 9));
        }

        [TestMethod]
        public void Day01BTests()
        {
            Do<Day01B>.Tests(
                P("1212", 6),
                P("1221", 0),
                P("123425", 4),
                P("123123", 12),
                P("12131415", 4));
        }

        [TestMethod]
        public void Day02ATests()
        {
            string i1 = @"5 1 9 5
7 5 3
2 4 6 8";
            Do<Day02A>.Tests(P(i1, 18));
        }

        [TestMethod]
        public void Day02BTests()
        {
            string i1 = @"5 9 2 8
9 4 7 3
3 8 6 5";
            Do<Day02B>.Tests(P(i1, 9));
        }

        [TestMethod]
        public void Day03ATests()
        {
            Do<Day03A>.Tests(
                P("1", 0),
                P("12", 3),
                P("23", 2),
                P("1024", 31));
        }

        [TestMethod]
        public void Day03BTests()
        {
            Do<Day03B>.Tests(
                P("1", 2),
                P("12", 23),
                P("23", 25),
                P("747", 806));
        }

        [TestMethod]
        public void Day04ATests()
        {
            string many = @"a b c d
e f g h
a a b c
a b c c";
            Do<Day04A>.Tests(
                P("aa bb cc dd ee", 1),
                P("aa bb cc dd aa", 0),
                P("aa bb cc dd aaa", 1),
                P(many, 2));
        }

        [TestMethod]
        public void Day04BTests()
        {
            string many = @"a b c d
e f g h
a a b c
a b ca ac";
            Do<Day04B>.Tests(
                P("abcde fghij", 1),
                P("abcde xyz ecdab", 0),
                P("a ab abc abd abf abj", 1),
                P("iiii oiii ooii oooi oooo", 1),
                P("oiii ioii iioi iiio", 0),
                P(many, 2));
        }

        [TestMethod]
        public void Day05ATests()
        {
            string i1 = @"0
3
0
1
-3";
            Do<Day05A>.Tests(
                P(i1, 5));
        }

        [TestMethod]
        public void Day05BTests()
        {
            string i1 = @"0
3
0
1
-3";
            Do<Day05B>.Tests(
                P(i1, 10));
        }

        [TestMethod]
        public void Day06ATests()
        {
            Do<Day06A>.Tests(
                P("0 2 7 0", 5));
        }

        [TestMethod]
        public void Day06BTests()
        {
            Do<Day06B>.Tests(
                P("0 2 7 0", 4));
        }

        [TestMethod]
        public void Day07ATests()
        {
            string i1 = @"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)";
            Do<Day07A>.Tests(
                P(i1, "tknk"));
        }

        [TestMethod]
        public void Day07BTests()
        {
            string i1 = @"pbga (66)
xhth (57)
ebii (61)
havc (66)
ktlj (57)
fwft (72) -> ktlj, cntj, xhth
qoyq (66)
padx (45) -> pbga, havc, qoyq
tknk (41) -> ugml, padx, fwft
jptl (61)
ugml (68) -> gyxo, ebii, jptl
gyxo (61)
cntj (57)";
            Do<Day07B>.Tests(
                P(i1, "ugml=60"));
        }

        [TestMethod]
        public void Day08ATests()
        {
            string i1 = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";
            Do<Day08A>.Tests(
                P(i1, 1));
        }

        [TestMethod]
        public void Day08BTests()
        {
            string i1 = @"b inc 5 if a > 1
a inc 1 if b < 5
c dec -10 if a >= 1
c inc -20 if c == 10";
            Do<Day08B>.Tests(
                P(i1, 10));
        }

        [TestMethod]
        public void Day09ATests()
        {
            Do<Day09A>.Tests(
                P("{}", 1),
                P("{{{}}}", 6),
                P("{{},{}}", 5),
                P("{{{},{},{{}}}}", 16),
                P("{<a>,<a>,<a>,<a>}", 1),
                P("{{<ab>},{<ab>},{<ab>},{<ab>}}", 9),
                P("{{<!!>},{<!!>},{<!!>},{<!!>}}", 9),
                P("{{<a!>},{<a!>},{<a!>},{<ab>}}", 3));
        }

        [TestMethod]
        public void Day09BTests()
        {
            Do<Day09B>.Tests(
                P("{<>}", 0),
                P("{<random characters>}", 17),
                P("{<<<<>}", 3),
                P("{<{!>}>}", 2),
                P("{<!!>}", 0),
                P("{<!!!>>}", 0),
                P("{<{o\"i!a,<{i<a>}", 10));
        }

        [TestMethod]
        public void Day10ATests()
        {
            Do<Day10A>.Tests(
                P("5:3,4,1,5", 12));
        }

        [TestMethod]
        public void Day10BTests()
        {
            Do<Day10B>.Tests(
                P(string.Empty, "a2582a3a0e66e6e86e3812dcb672a272"),
                P("AoC 2017", "33efeb34ea91902bb2f59c9920caa6cd"),
                P("1,2,3", "3efbe78a8d82f29979031a4aa0b16a9d"),
                P("1,2,4", "63960835bcdc130f0b66d7ff4f6a5a8e"));
        }

        private static KeyValuePair<string, string> P<TValue>(string key, TValue value)
        {
            return new KeyValuePair<string, string>(key, value.ToString());
        }

        private static class Do<TDay> where TDay : ICanRun, new()
        {
            public static void Tests(params KeyValuePair<string, string>[] pairs)
            {
                foreach (KeyValuePair<string, string> pair in pairs)
                {
                    string input = pair.Key;
                    string result = string.Empty;
                    Action act = () => result = new TDay().Run(pair.Key);

                    act.ShouldNotThrow("{0} was the input", input);
                    result.Should().Be(pair.Value, "{0} was the input", input);
                }
            }
        }

        private sealed class ADay : DayBase<string>
        {
            public override string DefaultInput => "-1";

            protected override string RunCore(string input) => "x" + int.Parse(input);
        }

        private sealed class BDay : DayBase<int>
        {
            public override string DefaultInput => "500";

            protected override int RunCore(string input) => int.Parse(input);
        }
    }
}
