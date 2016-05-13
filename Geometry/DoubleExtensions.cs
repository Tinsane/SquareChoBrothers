using System;

namespace Geometry
{
    public static class DoubleExtensions
    {
        public static bool IsDoubleEqual(this double x, double y)
        {
            return Math.Abs(x - y) < Geom.Precision;
        }

        public static bool IsDoubleLess(this double x, double y)
        {
            return !x.IsDoubleEqual(y) && x < y;
        }

        public static bool IsDoubleLessEqual(this double x, double y)
        {
            return x.IsDoubleEqual(y) || x < y;
        }

        public static bool IsDoubleGreater(this double x, double y)
        {
            return !x.IsDoubleLessEqual(y);
        }

        public static bool IsDoubleGeaterEqual(this double x, double y)
        {
            return !x.IsDoubleLess(y);
        }
    }
}