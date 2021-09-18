using System;
using System.Collections.Generic;
using System.Linq;
using ITDranik.CodingInterview.Solvers.Common;

namespace ITDranik.CodingInterview.Solvers.Geometry
{
    public class MaxPointsOnLine
    {
        public const int DefaultDigitsPrecision = 3;

        public static (Line<double> Line, int PointsCount) FindLine(List<Point<double>> points)
        {
            if (AllPointsAreAlmostEqual(points))
            {
                var point = points.FirstOrDefault() ?? new Point<double>(0, 0);
                var line = LinesFactory.BuildAnyLine(point);
                return (Line: line, PointsCount: points.Count);
            }

            var maxPointsOnLineCount = 0;
            var bestLine = new Line<double>(1, 0, 0);

            foreach (var firstPoint in points)
            {
                foreach (var secondPoint in points)
                {
                    if (secondPoint.IsAlmostEqual(firstPoint))
                    {
                        continue;
                    }

                    var line = LinesFactory.BuildLine(firstPoint, secondPoint);
                    var pointsOnLineCount = points.Count((p) => line.AlmostContains(p));

                    if (pointsOnLineCount > maxPointsOnLineCount)
                    {
                        maxPointsOnLineCount = pointsOnLineCount;
                        bestLine = line;
                    }
                }
            }

            return (Line: bestLine, PointsCount: maxPointsOnLineCount);
        }

        public static (Line<double> line, int PointsCount) FindLineFast(
            List<Point<double>> points,
            int digitsPrecision = DefaultDigitsPrecision)
        {
            double multiplier = Math.Pow(10, digitsPrecision);

            var longPoints = points.Select((p) => new Point<long>(
                Convert.ToInt64(Math.Round(p.X * multiplier)),
                Convert.ToInt64(Math.Round(p.Y * multiplier))
            )).ToList();

            (var longOrigin, var longDirection, var pointsCount) = FindVectorFast(longPoints);

            var origin = new Point<double>(
                longOrigin.X / multiplier,
                longOrigin.Y / multiplier
            );
            var direction = new Vector<double>(
                longDirection.X / multiplier,
                longDirection.Y / multiplier
            );

            return (LinesFactory.BuildLine(origin, direction), pointsCount);
        }

        public static (Line<long> line, int PointsCount) FindLineFast(List<Point<long>> points)
        {
            (var origin, var direction, var pointsCount) = FindVectorFast(points);
            var line = LinesFactory.BuildLine(origin, direction);
            return (line, pointsCount);
        }

        private static (Point<long> Origin, Vector<long> Direction, int PointsCount) FindVectorFast(
            List<Point<long>> points)
        {
            if (AllPointsAreEqual(points))
            {
                var origin = points.FirstOrDefault() ?? new Point<long>(0, 0);
                var direction = new Vector<long>(1, 1);
                return (Origin: origin, Direction: direction, PointsCount: points.Count);
            }

            var bestOrigin = new Point<long>(0, 0);
            var bestDirection = new Vector<long>(1, 1);
            var maxPointsCount = 0;

            foreach (var origin in points)
            {
                (var direction, var pointsCount) = FindDirectionWithMaxPointsCount(origin, points);
                if (pointsCount > maxPointsCount)
                {
                    bestOrigin = origin;
                    bestDirection = direction;
                    maxPointsCount = pointsCount;
                }
            }

            return (Origin: bestOrigin, Direction: bestDirection, PointsCount: maxPointsCount);
        }

        private static (Vector<long> Direction, int PointsCount) FindDirectionWithMaxPointsCount(
            Point<long> origin,
            List<Point<long>> points)
        {
            int originDuplicates = 0;
            var pointsCountPerDirection = new Dictionary<long, Dictionary<long, int>>();
            int maxPointsCount = 0;
            var bestDirection = new Vector<long>(1, 1);

            foreach (var linePoint in points)
            {
                if (linePoint.IsEqual(origin))
                {
                    ++originDuplicates;
                    continue;
                }

                var direction = NormalizeDirection(
                    VectorsFactory.BuildVector(origin, linePoint)
                );
                var pointsCount = AddDirectionAndGetPointsCount(
                    pointsCountPerDirection,
                    direction
                );

                if (pointsCount > maxPointsCount)
                {
                    maxPointsCount = pointsCount;
                    bestDirection = direction;
                }
            }

            return (Direction: bestDirection, PointsCount: maxPointsCount + originDuplicates);
        }

        private static bool AllPointsAreAlmostEqual(List<Point<double>> points)
        {
            var p1 = points.FirstOrDefault() ?? new Point<double>(0, 0);
            return points.All((p) => p.IsAlmostEqual(p1));
        }

        private static bool AllPointsAreEqual(List<Point<long>> points)
        {
            var p1 = points.FirstOrDefault() ?? new Point<long>(0, 0);
            return points.All((p) => p.IsEqual(p1));
        }

        private static Vector<long> NormalizeDirection(Vector<long> direction)
        {
            long gcdValue = Arithmetic.GCD(direction.X, direction.Y);
            return new Vector<long>(direction.X / gcdValue, direction.Y / gcdValue);
        }

        private static int AddDirectionAndGetPointsCount(
            Dictionary<long, Dictionary<long, int>> directionsCountPerOrigin,
            Vector<long> direction)
        {
            if (!directionsCountPerOrigin.TryGetValue(direction.X, out var fixedXCounts))
            {
                fixedXCounts = new Dictionary<long, int>();
                directionsCountPerOrigin[direction.X] = fixedXCounts;
            }

            if (!fixedXCounts.TryGetValue(direction.Y, out var count))
            {
                count = 0;
            }

            fixedXCounts[direction.Y] = count + 1;
            return count + 1;
        }
    }
}
