using System.Collections.Generic;
using FluentAssertions;
using ITDranik.CodingInterview.Solvers.Geometry;
using Xunit;

namespace ITDranik.CodingInterview.SolversTests.Geometry
{
    public class LineTests
    {
        public static IEnumerable<object[]> GetPointIsOnLineData()
        {
            var line = LinesFactory.BuildLine(
                new Point<double>(0, 0),
                new Point<double>(2, 3)
            );

            yield return new object[] { line, new Point<double>(0, 0) };
            yield return new object[] { line, new Point<double>(2, 3) };
            yield return new object[] { line, new Point<double>(4, 6) };
            yield return new object[] { line, new Point<double>(2 * 0.33, 3 * 0.33) };
        }

        [Theory]
        [MemberData(nameof(GetPointIsOnLineData))]
        public void PointIsOnLineTest(Line<double> line, Point<double> p)
        {
            line.AlmostContains(p).Should().BeTrue();
        }

        public static IEnumerable<object[]> GetPointIsNotOnlineData()
        {
            var line = LinesFactory.BuildLine(
                new Point<double>(0, 0),
                new Point<double>(2, 3)
            );

            yield return new object[] { line, new Point<double>(2, 2) };
            yield return new object[] { line, new Point<double>(0.2, 0.31) };
        }

        [Theory]
        [MemberData(nameof(GetPointIsNotOnlineData))]
        public void PointIsNotOnLineTest(Line<double> line, Point<double> p)
        {
            line.AlmostContains(p).Should().BeFalse();
        }

        [Fact]
        public void PrecisionTest()
        {
            var line = LinesFactory.BuildLine(
                new Point<double>(-1, -2),
                new Point<double>(2, 3)
            );

            var point = new Point<double>(2, 3.001);
            var precision = 1e-2;

            line.AlmostContains(point, precision).Should().BeTrue();
        }

        [Fact]
        public void EqualLinesTest()
        {
            var line1 = LinesFactory.BuildLine(
                new Point<double>(-1, -2),
                new Point<double>(3, 2)
            );

            var line2 = line1;
            line1.IsAlmostEqual(line2).Should().BeTrue();
        }

        [Fact]
        public void SameLineTests()
        {
            var line1 = LinesFactory.BuildLine(
                new Point<double>(-1, -2),
                new Point<double>(3, 2)
            );

            var line2 = new Line<double>(
                a: line1.A * 0.5,
                b: line1.B * 0.5,
                c: line1.C * 0.5
            );

            line2.IsAlmostSame(line1);
        }
    }
}
