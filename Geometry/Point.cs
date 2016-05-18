using System;
using System.Drawing;

namespace Geometry
{
    public class Point : Geom, IComparable<Point>
    {
        public double x;
        public double y;

        public Point()
        {
            x = 0;
            y = 0;
        }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Point(Point a)
        {
            x = a.x;
            y = a.y;
        }

        public int CompareTo(Point other)
        {
            if (this == other)
                return 0;
            return this < other ? -1 : 1;
        }

        public double GetDistance(Point point) => new Vector(this, point).Length;

        public bool IsOnSegment(Segment segment) => segment.Contains(this);

        /// <summary>
        ///     Вращает исходную точку относительно принимаемой методом точки на угол, заданный синусом
        ///     и косинусом.
        /// </summary>
        public void Rotate(Point point, double sin, double cos)
        {
            if (this == point)
                return;
            var toPointVector = new Vector(point, this);
            toPointVector.Rotate(sin, cos);
            x = point.x + toPointVector.x;
            y = point.y + toPointVector.y;
        }

        /// <summary>
        ///     Возвращает исходную точку, повёрнутую относительно принимаемой методом точки на угол,
        ///     заданный в синусом и косинусом.
        /// </summary>
        public Point GetRotated(Point point, double sin, double cos)
        {
            if (this == point)
                return new Point(x, y);
            var toPointVector = new Vector(point, this);
            toPointVector.Rotate(sin, cos);
            return point + toPointVector;
        }

        /// <summary>
        ///     Вращает исходную точку относительно принимаемой методом точки на угол, заданный в радианах.
        /// </summary>
        public void Rotate(Point point, double angle) => Rotate(point, Math.Sin(angle), Math.Cos(angle));

        /// <summary>
        ///     Возвращает исходную точку, повёрнутую относительно принимаемой методом точки на угол,
        ///     заданный в радианах.
        /// </summary>
        public Point GetRotated(Point point, double angle) => GetRotated(point, Math.Sin(angle), Math.Cos(angle));

        public static Point operator +(Point point, Vector vector) => new Point(point.x + vector.x, point.y + vector.y);

        public static Point operator -(Point point, Vector vector) => new Point(point.x - vector.x, point.y - vector.y);

        public static Point operator *(Point point, double coeff) => new Point(point.x*coeff, point.y*coeff);

        public static Point operator /(Point point, double coeff) => point*(1/coeff);

        public static bool operator ==(Point a, Point b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;
            return a.x.IsDoubleEqual(b.x) && a.y.IsDoubleEqual(b.y);
        }

        public static bool operator !=(Point a, Point b) => !(a == b);

        public static bool operator <(Point a, Point b)
        {
            return a.x.IsDoubleLess(b.x) ||
                   a.x.IsDoubleEqual(b.x) && a.y.IsDoubleLess(b.y);
        }

        public static bool operator >(Point a, Point b) => b < a;

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
                return false;
            return (Point) obj == this;
        }

        public static implicit operator Vector(Point point) => new Vector(point.x, point.y);

        public static implicit operator PointF(Point point) => new PointF((float) point.x, (float) point.y);

        public override string ToString() => $"x={x} y={y}";
    }
}