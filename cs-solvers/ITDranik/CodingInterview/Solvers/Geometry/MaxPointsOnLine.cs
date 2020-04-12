using System.Collections.Generic;

namespace ITDranik.CodingInterview.Solvers.Geometry
{
    public class MaxPointsOnLine
    {
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
    }
}
