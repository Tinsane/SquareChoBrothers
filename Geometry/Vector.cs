using System;

namespace Geometry
{
    public class Vector : Geom
    {
        public double x, y;

        public Vector(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector(Point a, Point b)
        {
            x = b.x - a.x;
            y = b.y - a.y;
        }

        public double PolarAngle => Math.Atan2(y, x);

        public double Length => Math.Sqrt(x*x + y*y);

        public bool IsZero => this == new Vector(0, 0);

        public Vector Reversed => new Vector(-x, -y);

        public void Normalize(double newLength)
        {
            var coefficient = newLength/Length;
            x *= coefficient;
            y *= coefficient;
        }

        public bool IsCollinear(Vector vector) => Math.Abs(GetDotProduct(vector)) < GetPrecision();

        public bool IsCodirectional(Vector vector) => IsCollinear(vector) && (GetScalarProduct(vector) > 0);

        public bool IsContradirectional(Vector vector) => IsCollinear(vector) && (GetScalarProduct(vector) < 0);

        /// <summary>
        ///     Вращает вектор против часовой стрелки на угол, заданный синусом и косинусом.
        /// </summary>
        public void Rotate(double sin, double cos)
        {
            var oldX = x;
            var oldY = y;
            x = oldX*cos - oldY*sin;
            y = oldX*sin + oldY*cos;
        }

        /// <summary>
        ///     Возвращает исходный вектор, повёрнутый против
        ///     часовой стрелки на угол, заданный синусом и косинусом.
        /// </summary>
        public Vector GetRotated(double sin, double cos) => new Vector(x*cos - y*sin, x*sin + y*cos);

        /// <summary>
        ///     Вращает вектор против часовой стрелки на угол, заданный в радианах.
        /// </summary>
        public void Rotate(double angle) => Rotate(Math.Sin(angle), Math.Cos(angle));

        /// <summary>
        ///     Возвращает исходный вектор, повёрнутый против часовой стрелки на угол, заданный в радианах.
        /// </summary>
        public Vector GetRotated(double angle) => GetRotated(Math.Sin(angle), Math.Cos(angle));

        public double GetScalarProduct(Vector vector) => x*vector.x + y*vector.y;

        public double GetDotProduct(Vector vector) => x*vector.y - y*vector.x;

        public double GetAngle(Vector vector) => Math.Atan2(GetDotProduct(vector), GetScalarProduct(vector));

        public void Reverse()
        {
            x = -x;
            y = -y;
        }

        public static Vector operator +(Vector a, Vector b) => new Vector(a.x + b.x, a.y + b.y);

        public static Vector operator -(Vector a, Vector b) => new Vector(a.x - b.x, a.y - b.y);

        public static Vector operator -(Vector a) => new Vector(0, 0) - a;

        public static Vector operator *(Vector a, double t) => new Vector(a.x*t, a.y*t);

        public static Vector operator /(Vector a, double t) => a*(1/t);

        public static bool operator ==(Vector a, Vector b)
        {
            if (ReferenceEquals(a, null))
                return ReferenceEquals(b, null);
            if (ReferenceEquals(b, null))
                return false;
            return (Math.Abs(a.x - b.x) < GetPrecision()) && (Math.Abs(a.y - b.y) < GetPrecision());
        }

        public static bool operator !=(Vector a, Vector b) => !(a == b);
    }
}