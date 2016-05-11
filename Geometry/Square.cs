using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Square : Geom, IGeometryFigure
    {
        private Point[] points;
        public Point A => new Point(points[0]);
        public Point B => new Point(points[1]);
        public Point C => new Point(points[2]);
        public Point D => new Point(points[3]);
        public double Size => A.GetDistance(B);
        public Point[] Points => points.Select(x => new Point(x)).ToArray();
        public Segment[] Segments => Points.Select((x, i) => 
            new Segment(x, Points[(i + 1) % 4])).ToArray();
        public Point Center => (A + C) / 2;

        public Square(Point center, double halfSideSize)
        {
            var dx = new[] { -1, -1, 1, 1 };
            var dy = new[] { 1, -1, 1, -1 };
            points = Geometry.OrderClockwise(dx.Select((t, i) =>
                new Point(center.x + t * halfSideSize, center.y + dy[i] * halfSideSize)).ToArray());
        }

        public static implicit operator System.Drawing.RectangleF(Square square)
        {
            return new RectangleF(square.A, new Size((int)square.Size, (int)square.Size));
        }

        public bool IntersectsWith(Circle circle)
        {
            return Segments.Any(x => x.IntersectsWith(circle));
        }

        public Line GetIntersectionLine(Circle circle)
        {
            return IntersectsWith(circle) ? 
                new Segment(Center, circle.Center).GetMiddlePerpendicular() : null;
        }

        public void Transfer(Vector transferVector)
        {
            for (var i = 0; i < points.Length; ++i)
                points[i] += transferVector;
        }

        public Square GetTransfered(Vector transferVector)
        {
            return new Square(Center + transferVector, Size);
        }

        public bool Contains(Point point)
        {
            if (Segments.Any(x => x.Contains(point)))
                return true;
            var angle = points.Select((t, i) => 
                new Vector(point, t).GetAngle(new Vector(point, points[(i + 1) % 4]))).Sum();
            return (Math.Abs(angle) - Math.PI * 2) < precision;
        }

        public bool IntersectsWith(Segment segment)
        {
            return Contains(segment.A) || Contains(segment.B);
        }

        public bool IntersectsWith(Square square)
        {
            return Segments.Any(square.IntersectsWith);
        }

        public Line GetIntersectionLine(Square square)
        {
            return IntersectsWith(square) ?
                new Segment(Center, square.Center).GetMiddlePerpendicular() : null;
        }
    }
}
