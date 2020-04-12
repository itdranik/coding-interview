using System.Collections.Generic;
using FluentAssertions;
using ITDranik.CodingInterview.Solvers.Geometry;
using Xunit;

namespace ITDranik.CodingInterview.SolversTests.Geometry
{
    public class MaxPointsOnLineTests
    {
        public MaxPointsOnLineTests()
        {
            _solver = new MaxPointsOnLine();
        }

        [Fact]
        public void NoPointsTest()
        {
            (var line, var pointsCount) = _solver.FindLine(new List<Point<double>>());
            line.Should().BeNull();
            pointsCount.Should().Be(0);
        }

        [Fact]
        public void SeveralEqualPointsTest()
        {
            (var line, var pointsCount) = _solver.FindLine(new List<Point<double>> {
                new Point<double>(1, 1),
                new Point<double>(1, 1),
                new Point<double>(1, 1)
            });
            line.Should().BeNull();
            pointsCount.Should().Be(0);
        }

        [Fact]
        public void TwoPointsTest()
        {
            (var line, var pointsCount) = _solver.FindLine(new List<Point<double>> {
                new Point<double>(1, 2),
                new Point<double>(-3, 5)
            });

            line!.Value.IsAlmostSame(new Line<double>(4, 5, -14));
            pointsCount.Should().Be(2);
        }

        [Fact]
        public void MultipleEqualPointsOnLineTest()
        {
            (var line, var pointsCount) = _solver.FindLine(new List<Point<double>> {
                new Point<double>(1, 2),
                new Point<double>(1, 2),
                new Point<double>(-3, 5),
                new Point<double>(-3, 5)
            });

            line!.Value.IsAlmostSame(new Line<double>(4, 5, -14));
            pointsCount.Should().Be(4);
        }

        [Fact]
        public void SeveralLinesTest()
        {
            (var line, var pointsCount) = _solver.FindLine(new List<Point<double>> {
                new Point<double>(1, 1),
                new Point<double>(4, 1),
                new Point<double>(4, 5),
                new Point<double>(5, 3),
                new Point<double>(7, 4)
            });

            line!.Value.IsAlmostSame(new Line<double>(1, -2, 1));
            pointsCount.Should().Be(3);
        }

        private readonly MaxPointsOnLine _solver;
    }
}
