using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Rectangle : Geom, IGeometryFigure
    {
        private Point[] points;
        /// <summary>
        /// Левая нижняя точка
        /// </summary>
        public Point A => new Point(points[0]);
        /// <summary>
        /// Левая верхняя точка
        /// </summary>
        public Point B => new Point(points[1]);
        /// <summary>
        /// Правая верхняя точка
        /// </summary>
        public Point C => new Point(points[2]);
        /// <summary>
        /// Правая нижняя точка
        /// </summary>
        public Point D => new Point(points[3]);

        public double SizeX => A.GetDistance(B);

        public double SizeY => B.GetDistance(C);

        public Point[] Points => points.Select(x => new Point(x)).ToArray();

        public Segment[] Segments => Points.Select((x, i) => 
            new Segment(x, Points[(i + 1) % 4])).ToArray();

        public Point Center => (A + C) / 2;

        public Rectangle(Point center, double sizeX, double sizeY)
        {
            var dx = new[] { -1, -1, 1, 1 };
            var dy = new[] { 1, -1, 1, -1 };
            points = Geometry.OrderClockwise(dx.Select((t, i) =>
                new Point(center.x + t * sizeX / 2, center.y + dy[i] * sizeY / 2)).ToArray());
        }

        public static implicit operator System.Drawing.RectangleF(Rectangle rectangle)
        {
            return new RectangleF(rectangle.A, new Size((int)rectangle.SizeX, (int)rectangle.SizeY));
        }

        public bool IntersectsWith(Circle circle)
        {
            return Segments.Any(x => x.IntersectsWith(circle));
        }

        public Line GetIntersectionLine(Circle circle)
        {
            if (!IntersectsWith(circle))
                return null;
            return GetIntersectionLine(new Rectangle(circle.Center, circle.r * 2, circle.r * 2));
        }

        public void Transfer(Vector transferVector)
        {
            for (var i = 0; i < points.Length; ++i)
                points[i] += transferVector;
        }

        public Rectangle GetTransfered(Vector transferVector)
        {
            return new Rectangle(Center + transferVector, SizeX, SizeY);
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

        public bool IntersectsWith(Rectangle rectangle)
        {
            return Segments.Any(rectangle.IntersectsWith);
        }

        public Line GetIntersectionLine(Rectangle rectangle)
        {
            if (!IntersectsWith(rectangle))
                return null;
            var coeffX = Math.Abs(Center.x - rectangle.Center.x) / (SizeX + rectangle.SizeX);
            var coeffY = Math.Abs(Center.y - rectangle.Center.y) / (SizeY + rectangle.SizeY);
            if (coeffX < coeffY)
            {
                if (A.GetDistance(rectangle.Center) < C.GetDistance(rectangle.Center))
                    return new Line(A, B);
                else
                    return new Line(C, D);
            }
            else
            {
                if (A.GetDistance(rectangle.Center) < B.GetDistance(rectangle.Center))
                    return new Line(A, D);
                else
                    return new Line(B, C);
            }
        }
    }
}
