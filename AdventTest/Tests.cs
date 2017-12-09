namespace AdventTest
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
    public sealed class Tests
    {
        [TestMethod]
        public void X()
        {
            int x = 1;
            int y = 2;

            int sum = x + y;

            sum.Should().Be(3);
        }
    }
}
