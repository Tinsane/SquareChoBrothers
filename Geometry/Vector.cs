using System;
using Newtonsoft.Json;

namespace Geometry
{
    [JsonObject(MemberSerialization.Fields)]
    public class Vector : Geom
    {
        public double x, y;

        static Vector()
        {
            OxDirectional = new Vector(1, 0);
            OyDirectional = new Vector(0, 1);
            ZeroVector = new Vector(0, 0);
        }

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

        public static Vector OxDirectional { get; }
        public static Vector OyDirectional { get; }
        public static Vector ZeroVector { get; }
        public Vector OxProjection => OxDirectional*GetScalarProduct(OxDirectional);
        public Vector OyProjection => OyDirectional*GetScalarProduct(OyDirectional);

        public Vector()
        {
            x = y = 0;
        }

        public double PolarAngle => Math.Atan2(y, x);

        public double Length => Math.Sqrt(x*x + y*y);

        public bool IsZero => this == ZeroVector;

        public Vector Reversed => -this;

        public Vector GetNormalized(double length) => this/Length*length;

        public void Normalize(double newLength)
        {
            var coefficient = newLength/Length;
            x *= coefficient;
            y *= coefficient;
        }

        public bool IsCollinear(Vector vector) => Math.Abs(GetDotProduct(vector)).IsDoubleEqual(0);

        public bool IsCodirectional(Vector vector) => IsCollinear(vector) && GetScalarProduct(vector).IsDoubleGreater(0);

        public bool IsContradirectional(Vector vector)
            => IsCollinear(vector) && GetScalarProduct(vector).IsDoubleLess(0);

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

        public static Vector operator +(Vector a, Vector b) => new Vector(a.x + b.x, a.y + b.y);

        public static Vector operator -(Vector a, Vector b) => new Vector(a.x - b.x, a.y - b.y);

        public static Vector operator -(Vector a) => ZeroVector - a;

        public static Vector operator *(Vector a, double t) => new Vector(a.x*t, a.y*t);

        public static Vector operator /(Vector a, double t) => a*(1/t);

        public static bool operator ==(Vector a, Vector b)
        {
            if (ReferenceEquals(a, null))
                return ReferenceEquals(b, null);
            if (ReferenceEquals(b, null))
                return false;
            return a.x.IsDoubleEqual(b.x) && a.y.IsDoubleEqual(b.y);
        }

        public static bool operator !=(Vector a, Vector b) => !(a == b);

        public Vector GetReflected(Line line)
        {
            var collinearComponent = GetProjection(line);
            var normalComponent = this - collinearComponent;
            return collinearComponent - normalComponent;
        }

        public Vector GetProjection(Line line)
        {
            var directionVector = line.DirectionVector;
            return directionVector.GetNormalized(directionVector.GetScalarProduct(this));
        }
    }
}