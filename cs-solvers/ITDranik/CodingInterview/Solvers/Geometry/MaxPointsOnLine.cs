using System;
using System.Collections.Generic;
using System.Linq;
using ITDranik.CodingInterview.Solvers.Common;

namespace ITDranik.CodingInterview.Solvers.Geometry
{
    public class MaxPointsOnLine
    {
        public const int DefaultDigitsPrecision = 3;

        public (Line<double>? Line, int PointsCount) FindLine(List<Point<double>> points)
        {
            var maxPointsOnLineCount = 0;
            Line<double>? bestLine = null;

            foreach (var firstPoint in points)
            {
                foreach (var secondPoint in points)
                {
                    if (secondPoint.IsAlmostEqual(firstPoint))
                    {
                        continue;
                    }

                    var line = LinesFactory.BuildLine(firstPoint, secondPoint);
                    int pointsOnLineCount = 0;

                    foreach (var point in points)
                    {
                        if (line.AlmostContains(point))
                        {
                            ++pointsOnLineCount;
                        }
                    }

                    if (pointsOnLineCount > maxPointsOnLineCount)
                    {
                        maxPointsOnLineCount = pointsOnLineCount;
                        bestLine = line;
                    }
                }
            }

            return (Line: bestLine, PointsCount: maxPointsOnLineCount);
        }

        public (Line<double>? line, int PointsCount) FindLineFast(
            List<Point<double>> points,
            int digitsPrecision = DefaultDigitsPrecision)
        {
            double multiplier = Math.Pow(10, digitsPrecision);

            var longPoints = points.Select((p) => new Point<long>(
                Convert.ToInt64(Math.Round(p.X * multiplier)),
                Convert.ToInt64(Math.Round(p.Y * multiplier))
            )).ToList();

            (var longOrigin, var longDirection, var pointsCount) = FindVectorFast(longPoints);

            if (!longOrigin.HasValue || !longDirection.HasValue)
            {
                return (null, 0);
            }

            var origin = new Point<double>(
                longOrigin.Value.X / multiplier,
                longOrigin.Value.Y / multiplier
            );
            var direction = new Vector<double>(
                longDirection.Value.X / multiplier,
                longDirection.Value.Y / multiplier
            );

            return (LinesFactory.BuildLine(origin, direction), pointsCount);
        }

        public (Line<long>? line, int PointsCount) FindLineFast(List<Point<long>> points)
        {
            (var origin, var direction, var pointsCount) = FindVectorFast(points);
            if (!origin.HasValue || !direction.HasValue)
            {
                return (null, 0);
            }

            var line = LinesFactory.BuildLine(origin.Value, direction.Value);
            return (line, pointsCount);
        }

        private (Point<long>? Origin, Vector<long>? Direction, int PointsCount) FindVectorFast(
            List<Point<long>> points)
        {
            Point<long>? bestOrigin = null;
            Vector<long>? bestVector = null;
            var maxPointsOnLineCount = 0;

            foreach (var origin in points)
            {
                var originDuplicates = 0;
                var directionsCountPerOrigin = new Dictionary<long, Dictionary<long, int>>();
                var maxDirectionsCount = 0;
                Vector<long>? bestDirectionForOrigin = null;

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
                    var directionsCount = AddDirectionAndGetCount(
                        directionsCountPerOrigin,
                        direction
                    );

                    if (directionsCount > maxDirectionsCount)
                    {
                        maxDirectionsCount = directionsCount;
                        bestDirectionForOrigin = direction;
                    }
                }

                int pointsOnLineCountForOrigin = bestDirectionForOrigin.HasValue
                    ? maxDirectionsCount + originDuplicates
                    : 0;

                if (pointsOnLineCountForOrigin > maxPointsOnLineCount)
                {
                    bestOrigin = origin;
                    bestVector = bestDirectionForOrigin;
                    maxPointsOnLineCount = pointsOnLineCountForOrigin;
                }
            }

            return (bestOrigin, bestVector, maxPointsOnLineCount);
        }

        private Vector<long> NormalizeDirection(Vector<long> direction)
        {
            long gcdValue = Arithmetic.GCD(direction.X, direction.Y);
            return new Vector<long>(direction.X / gcdValue, direction.Y / gcdValue);
        }

        private int AddDirectionAndGetCount(
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
