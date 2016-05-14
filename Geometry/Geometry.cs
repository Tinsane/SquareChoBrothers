using System;
using System.Linq;

namespace Geometry
{
    public static class Geometry
    {
        public static void Transfer(Vector transferVector, Point[] points)
        {
            for (var i = 0; i < points.Length; ++i)
                points[i] = points[i] + transferVector;
        }

        public static void Transfer(Vector transferVector, Circle[] circles)
        {
            circles = circles.Select(circle => circle + transferVector).ToArray();
        }

        public static void Scale(double scale, Point[] points)
        {
            points = points.Select(point => point * scale).ToArray();
        }

        public static void Scale(double scale, Circle[] circles)
        {
            foreach (var circle in circles)
            {
                circle.x *= scale;
                circle.y *= scale;
                circle.r *= scale;
            }
        }

        public static bool StrictlyIntersectsWith(this IGeometryFigure a, IGeometryFigure b)
        {
            var aType = a.GetType();
            var bType = b.GetType();
            var intersectMethod = aType.GetMethod("StrictlyIntersectsWith", new Type[] {bType});
            return (bool)intersectMethod.Invoke(a, new object[] {b});
        }

        public static Line GetIntersectionLine(this IGeometryFigure a, IGeometryFigure b)
        {
            var aType = a.GetType();
            var bType = b.GetType();
            var getLineMethod = aType.GetMethod("GetIntersectionLine", new Type[] {bType});
            return (Line)getLineMethod.Invoke(a, new object[] { b });
        }

        public static Segment CreateSegment(Point a, Point b) => new Segment(a, b);

        //public static Rectangle CreateRectangle(Point A, Point B, Point C, Point D)
        //{
        //    return new Rectangle(A, B, C, D);
        //}
        //
        //public static bool IsRectangle(Rectangle rect)
        //{
        //    return IsRectangle(rect.A, rect.B, rect.C, rect.D);
        //}

        public static bool IsRectangle(Point A, Point B, Point C, Point D)
        {
            var points = OrderClockwise(A, B, C, D);
            return IsRectangle(points);
        }

        public static bool IsRectangle(Point[] points)
        {
            return (new Vector(points[0], points[1]) == new Vector(points[3], points[2])) &&
                   (new Vector(points[1], points[2]) == new Vector(points[0], points[3])) &&
                   (Math.Abs(new Vector(points[0], points[1]).GetScalarProduct
                       (new Vector(points[0], points[3]))) < Geom.GetPrecision());
        }

        public static bool PointInInsideSegment(Point point, Segment segment)
        {
            return segment.Contains(point);
        }

        public static Point[] OrderClockwise(params Point[] points)
        {
            for (var i = 0; i < points.Length; ++i)
                if (points[0] > points[i])
                {
                    var forSwap = points[0];
                    points[0] = points[i];
                    points[i] = forSwap;
                }
            for (var i = 1; i < points.Length; ++i)
                for (var j = i + 1; j < points.Length; ++j)
                {
                    var toI = new Vector(points[0], points[i]);
                    var toJ = new Vector(points[0], points[j]);
                    if (toI.GetDotProduct(toJ) > 0)
                    {
                        var forSwap = points[i];
                        points[i] = points[j];
                        points[j] = forSwap;
                    }
                }
            return points;
        }
    }
}