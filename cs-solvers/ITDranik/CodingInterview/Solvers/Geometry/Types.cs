using System;

namespace ITDranik.CodingInterview.Solvers.Geometry
{
    public readonly struct Point<T> where T : struct
    {
        public Point(T x, T y)
        {
            X = x;
            Y = y;
        }

        public T X { get; }
        public T Y { get; }
    }

    public readonly struct Line<T> where T : struct
    {
        public Line(T a, T b, T c)
        {
            A = a;
            B = b;
            C = c;
        }

        public T A { get; }
        public T B { get; }
        public T C { get; }
    }

    public static class MathConstants
    {
        public const double DefaultPrecision = 1e-7;
    }

    public static class PointExtensions
    {
        public static bool IsAlmostEqual(
            this Point<double> current,
            Point<double> other,
            double precision = MathConstants.DefaultPrecision)
        {
            return Math.Abs(current.X - other.X) < precision
                && Math.Abs(current.Y - other.Y) < precision;
        }
    }

    public static class LineExtensions
    {
        public static bool AlmostContains(
            this Line<double> line,
            Point<double> point,
            double precision = MathConstants.DefaultPrecision)
        {
            var result = line.A * point.X + line.B * point.Y + line.C;
            return Math.Abs(result) < precision;
        }

        public static bool IsAlmostEqual(
            this Line<double> current,
            Line<double> other,
            double precision = MathConstants.DefaultPrecision)
        {
            return Math.Abs(current.A - other.A) < precision
                && Math.Abs(current.B - other.B) < precision
                && Math.Abs(current.C - other.C) < precision;
        }

        public static bool IsAlmostSame(
            this Line<double> current,
            Line<double> other,
            double precision = MathConstants.DefaultPrecision)
        {
            bool XYHaveEqualRatios(double x1, double y1, double x2, double y2)
            {
                return (x1 * y2 - x2 * y1) < precision;
            }

            return XYHaveEqualRatios(current.A, current.B, other.A, other.B)
                && XYHaveEqualRatios(current.A, current.C, other.A, other.C);
        }
    }

    public static class LinesFactory
    {
        public static Line<double> BuildLine(Point<double> p1, Point<double> p2)
        {
            var a = p2.Y - p1.Y;
            var b = p1.X - p2.X;
            var c = p2.X * p1.Y - p1.X * p2.Y;

            return new Line<double>(a, b, c);
        }
    }
}
