﻿using System;

namespace Geometry
{
    public class Line : Geom
    {
        public double a, b, c;

        public Line(Point firstPoint, Point secondPoint)
        {
            var directionVector = new Vector(firstPoint, secondPoint);
            a = directionVector.y;
            b = -directionVector.x;
            c = -a*firstPoint.x - b*firstPoint.y;
        }

        public Vector GetDirectionVector => new Vector(b, -a);

        public Vector GetNormalVector => new Vector(a, b);

        public Point PointOnLine => Math.Abs(b) < GetPrecision() ? new Point(-c/a, 0) : new Point(0, -c/b);

        public double GetDistance(Point point) => Math.Abs((a*point.x + b*point.y + c)/Math.Sqrt(a*a + b*b));

        /// <summary>
        ///     Возвращает значение выражения, получаемого подстановкой координат точки в уравнение прямой.
        /// </summary>
        public double GetValueOfLineEquation(Point point) => a*point.x + b*point.y + c;

        public bool ContainsPoint(Point point) => Math.Abs(GetValueOfLineEquation(point)) < GetPrecision();

        public bool IsParallel(Line line)
        {
            var firstLineDirectionVector = GetDirectionVector;
            var secondLineDirectionVector = line.GetDirectionVector;
            firstLineDirectionVector.Normalize(secondLineDirectionVector.Length);
            if (!(firstLineDirectionVector - secondLineDirectionVector).IsZero)
                firstLineDirectionVector = firstLineDirectionVector*-1.0;
            var difference = firstLineDirectionVector - secondLineDirectionVector;
            return difference.IsZero;
        }

        public Point Intersect(Line line)
        {
            if (IsParallel(line))
                return new Point(double.NaN, double.NaN);
            var a = new Vector(line.a, this.a);
            var b = new Vector(line.b, this.b);
            var c = new Vector(line.c, this.c);
            return new Point(-c.GetDotProduct(b)/a.GetDotProduct(b), -a.GetDotProduct(c)/a.GetDotProduct(b));
        }

        public static bool operator ==(Line a, Line b)
        {
            if (ReferenceEquals(a, null))
                return ReferenceEquals(b, null);
            if (ReferenceEquals(b, null))
                return false;
            return a.IsParallel(b) && b.ContainsPoint(a.PointOnLine);
        }

        public static bool operator !=(Line a, Line b) => !(a == b);
    }
}