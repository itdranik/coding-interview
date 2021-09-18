using System;
using System.Collections.Generic;
using FluentAssertions;
using ITDranik.CodingInterview.Solvers.Geometry;
using Xunit;

namespace ITDranik.CodingInterview.SolversTests.Geometry
{
    using SolverMethod = Func<List<Point<double>>, (Line<double>, int)>;

    public class MaxPointsOnLineTests
    {
        public static IEnumerable<object[]> GetSolvers()
        {
            SolverMethod findLine = (points) => MaxPointsOnLine.FindLine(points);
            yield return new object[] { findLine };

            SolverMethod findLineFast = (points) => MaxPointsOnLine.FindLineFast(points);
            yield return new object[] { findLineFast };
        }

        [Theory]
        [MemberData(nameof(GetSolvers))]
        public void NoPointsTest(SolverMethod solve)
        {
            (var _, var pointsCount) = solve(new List<Point<double>>());
            pointsCount.Should().Be(0);
        }

        [Theory]
        [MemberData(nameof(GetSolvers))]
        public void SeveralEqualPointsTest(SolverMethod solve)
        {
            (var line, var pointsCount) = solve(new List<Point<double>> {
                new Point<double>(1, 1),
                new Point<double>(1, 1),
                new Point<double>(1, 1)
            });
            line.AlmostContains(new Point<double>(1, 1));
            pointsCount.Should().Be(3);
        }

        [Theory]
        [MemberData(nameof(GetSolvers))]
        public void TwoPointsTest(SolverMethod solve)
        {
            (var line, var pointsCount) = solve(new List<Point<double>> {
                new Point<double>(1, 2),
                new Point<double>(-3, 5)
            });

            line.IsAlmostSame(new Line<double>(4, 5, -14));
            pointsCount.Should().Be(2);
        }

        [Theory]
        [MemberData(nameof(GetSolvers))]
        public void MultipleEqualPointsOnLineTest(SolverMethod solve)
        {
            (var line, var pointsCount) = solve(new List<Point<double>> {
                new Point<double>(1, 2),
                new Point<double>(1, 2),
                new Point<double>(-3, 5),
                new Point<double>(-3, 5)
            });

            line.IsAlmostSame(new Line<double>(4, 5, -14));
            pointsCount.Should().Be(4);
        }

        [Theory]
        [MemberData(nameof(GetSolvers))]
        public void SeveralLinesTest(SolverMethod solve)
        {
            (var line, var pointsCount) = solve(new List<Point<double>> {
                new Point<double>(1, 1),
                new Point<double>(4, 1),
                new Point<double>(4, 5),
                new Point<double>(5, 3),
                new Point<double>(7, 4)
            });

            line.IsAlmostSame(new Line<double>(1, -2, 1));
            pointsCount.Should().Be(3);
        }
    }
}
