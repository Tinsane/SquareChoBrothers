using System;
using System.Drawing;
using System.Linq;

namespace Geometry
{
    public class Rectangle : Geom, IGeometryFigure
    {
        private Point[] points;

        public Rectangle(Point center, double width, double height)
        {
            var dx = new[] {-1, -1, 1, 1};
            var dy = new[] {1, -1, 1, -1};
            points = Geometry.OrderClockwise(dx.Select((t, i) =>
                new Point(center.x + t*width/2, center.y + dy[i]*height/2)).ToArray());
        }

        /// <summary>
        ///     Левая нижняя точка
        /// </summary>
        public Point A => new Point(points[0]);

        /// <summary>
        ///     Левая верхняя точка
        /// </summary>
        public Point B => new Point(points[1]);

        /// <summary>
        ///     Правая верхняя точка
        /// </summary>
        public Point C => new Point(points[2]);

        /// <summary>
        ///     Правая нижняя точка
        /// </summary>
        public Point D => new Point(points[3]);

        public double Width => B.GetDistance(C);

        public double Height => A.GetDistance(B);

        public double SizeX => Width;

        public double SizeY => Height;

        public double Area => Width*Height;

        public Point[] Points => points.Select(x => new Point(x)).ToArray();

        public Segment[] Segments => Points.Select((x, i) =>
            new Segment(x, Points[(i + 1)%4])).ToArray();

        public Point Center => (A + C)/2;

        public bool IntersectsWith(Circle circle) =>
            Segments.Any(x => x.IntersectsWith(circle)) ||
            Contains(circle.Center) ||
            circle.Contains(Center);

        public Line GetIntersectionLine(Circle circle) => !IntersectsWith(circle)
            ? null
            : GetIntersectionLine(new Rectangle(circle.Center, circle.r*2, circle.r*2));

        public void Transfer(Vector transferVector)
        {
            points = points.Select(point => point + transferVector).ToArray();
        }

        public bool IntersectsWith(Rectangle rectangle)
            => Points.Any(rectangle.Contains) || rectangle.Points.Any(Contains);

        public Line GetIntersectionLine(Rectangle rectangle)
        {
            if (!IntersectsWith(rectangle))
                return null;
            var coeffX = Math.Abs(Center.x - rectangle.Center.x)/(SizeX + rectangle.SizeX);
            var coeffY = Math.Abs(Center.y - rectangle.Center.y)/(SizeY + rectangle.SizeY);
            if (coeffX > coeffY)
                return A.GetDistance(rectangle.Center) < C.GetDistance(rectangle.Center)
                    ? new Line(A, B)
                    : new Line(C, D);
            return A.GetDistance(rectangle.Center) < B.GetDistance(rectangle.Center) ? new Line(A, D) : new Line(B, C);
        }

        IGeometryFigure IGeometryFigure.GetTransfered(Vector transferVector)
            => new Rectangle(Center + transferVector, Width, Height);

        public Rectangle GetCopy() => new Rectangle(Center, Width, Height);

        public static implicit operator RectangleF(Rectangle rectangle) =>
            new RectangleF(rectangle.A, new Size((int) rectangle.Width, (int) rectangle.Height));

        public Rectangle GetTransfered(Vector transferVector) => new Rectangle(Center + transferVector, Width, Height);

        public bool Contains(Point point)
        {
            if (Segments.Any(x => x.Contains(point)))
                return true;
            var angle = points.Select((t, i) =>
                new Vector(point, t).GetAngle(new Vector(point, points[(i + 1)%4]))).Sum();
            return Math.Abs(angle).IsDoubleEqual(Math.PI*2);
        }

        public bool IntersectsWith(Segment segment) =>
            Contains(segment.A) ||
            Contains(segment.B) ||
            Segments.Any(s => s.IntersectsWith(segment));

        public static bool operator ==(Rectangle a, Rectangle b)
        {
            if (ReferenceEquals(a, null))
                return ReferenceEquals(b, null);
            if (ReferenceEquals(b, null))
                return false;
            return a.Center == b.Center && a.SizeX.IsDoubleEqual(b.SizeX) && a.SizeY.IsDoubleEqual(b.SizeY);
        }

        public static bool operator !=(Rectangle a, Rectangle b)
        {
            return !(a == b);
        }
    }
}