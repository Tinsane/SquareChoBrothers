using System;

namespace Geometry
{
    public class Segment : Geom
    {
        public Point A, B;

        public Segment(Point a, Point b)
        {
            A = a;
            B = b;
        }

        public Point Center => (A + B)/2;

        public Segment Reversed => new Segment(B, A);

        public double Length => A.GetDistance(B);

        public Line ContainingLine => new Line(A, B);

        public Line MiddlePerpendicular => new Line(Center, Center + new Line(A, B).NormalVector);

        /// <summary>
        ///     Возвращает точку на отрезку, делящую его в заданном отношении считая от вершины A.
        /// </summary>
        public Point GetDividingPointByRatio(double ratio)
        {
            if (ratio < 0)
                ratio = 0;
            if (ratio > 1)
                ratio = 1;
            var fromAtoB = new Vector(A, B);
            fromAtoB.Normalize(ratio*Length);
            return A + fromAtoB;
        }

        /// <summary>
        ///     Возвращает точку на отрезке, находящуюся на заданном расстоянии от вершины A.
        /// </summary>
        public Point GetPointOnSegmentOnDistanceFromA(double distance)
        {
            var ratio = distance/Length;
            return GetDividingPointByRatio(ratio);
        }

        /// <summary>
        ///     Возвращает подотрезок, образованный точками, делящими заданный отрезок в заданном отношении.
        /// </summary>
        public Segment GetSubsegmentByDividingPoints(double ratio1, double ratio2) =>
            new Segment(GetDividingPointByRatio(ratio1), GetDividingPointByRatio(ratio2));

        /// <summary>
        ///     Возвращает подотрезок, образованный точками, лежащими на заданных расстояниях от точки A.
        /// </summary>
        public Segment GetSubsegmentOnDistancesFromA(double distance1, double distance2) =>
            new Segment(GetPointOnSegmentOnDistanceFromA(distance1),
                GetPointOnSegmentOnDistanceFromA(distance2));

        private bool IsOnFirstSide(Point point)
        {
            var firstVector = new Vector(A, B);
            var secondVector = new Vector(A, point);
            return firstVector.GetScalarProduct(secondVector) < 0;
        }

        private bool IsOnSecondSize(Point point) => Reversed.IsOnFirstSide(point);

        public bool IntersectsWith(Segment segment) => !double.IsNaN(Intersect(segment).x) ||
                                                       segment.Contains(A) ||
                                                       segment.Contains(B) ||
                                                       Contains(segment.A) ||
                                                       Contains(segment.B);

        public bool IntersectsWith(Circle circle) // intersects or inside
            => circle.Contains(A) || circle.Contains(B);

        public Point Intersect(Segment segment)
        {
            var intersection = ContainingLine.Intersect(segment.ContainingLine);
            if (!(Contains(intersection) && segment.Contains(intersection)))
                return new Point(double.NaN, double.NaN);
            return intersection;
        }

        public double GetDistance(Point point)
        {
            if (IsOnFirstSide(point))
                return point.GetDistance(A);
            if (IsOnSecondSize(point))
                return point.GetDistance(B);
            var line = ContainingLine;
            return line.GetDistance(point);
        }

        public double GetDistance(Segment segment)
        {
            if (IntersectsWith(segment))
                return 0;
            return Math.Min(Math.Min(GetDistance(segment.A), GetDistance(segment.B)),
                Math.Min(segment.GetDistance(A), segment.GetDistance(B)));
        }

        public bool Contains(Point point)
        {
            var vA = new Vector(point, A);
            var vB = new Vector(point, B);
            return vA.GetDotProduct(vB).IsDoubleEqual(0) && vA.GetDotProduct(vB).IsDoubleLessEqual(0);
        }
    }
}