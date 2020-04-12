using System;

namespace ITDranik.CodingInterview.Solvers.Common
{
    public static class Arithmetic
    {
        public static int GCD(int x, int y)
        {
            int cx = Math.Abs(x);
            int cy = Math.Abs(y);

            while (cx != 0)
            {
                (cx, cy) = (cy % cx, cx);
            }

            return (x, y) switch
            {
                (int xv, _) when xv < 0 => -cy,
                (0, int yv) when yv < 0 => -cy,
                _ => cy
            };
        }

        public static long GCD(long x, long y)
        {
            long cx = Math.Abs(x);
            long cy = Math.Abs(y);

            while (cx != 0)
            {
                (cx, cy) = (cy % cx, cx);
            }

            return (x, y) switch
            {
                (long xv, _) when xv < 0 => -cy,
                (0, long yv) when yv < 0 => -cy,
                _ => cy
            };
        }
    }
}
