using FluentAssertions;
using ITDranik.CodingInterview.Solvers.Common;
using Xunit;

namespace ITDranik.CodingInterview.SolversTests.Common
{
    public class ArithmeticTests
    {
        [Theory]
        [InlineData( 10,  7,  1)]
        [InlineData( 22, 33, 11)]
        [InlineData(  0, 25, 25)]
        [InlineData( 25,  0, 25)]
        [InlineData(105, 54,  3)]
        public void GCDCalculationTest(int x, int y, int expected)
        {
            var evaluated = Arithmetic.GCD(x, y);
            evaluated.Should().Be(expected);
        }

        [Theory]
        [InlineData(-10, -10, -1)]
        [InlineData(-10,  10, -1)]
        [InlineData( 10, -10,  1)]
        [InlineData( 10,  10,  1)]
        [InlineData(-10,  -4, -1)]
        [InlineData(-10,   4, -1)]
        [InlineData( 10,  -4,  1)]
        [InlineData( 10,   4,  1)]
        public void GCDSignTest(int x, int y, int expected)
        {
            var evaluated = Arithmetic.GCD(x, y);
            (evaluated * expected).Should().BePositive();
        }
    }
}
