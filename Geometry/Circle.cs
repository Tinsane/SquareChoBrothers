namespace Geometry
{
    public class Circle : Geom, IGeometryFigure
    {
        public double x, y, r;

        public Circle(double x, double y, double radius)
        {
            this.x = x;
            this.y = y;
            r = radius;
        }

        public Circle(Point center, double radius)
        {
            x = center.x;
            y = center.y;
            r = radius;
        }

        public Point Center
        {
            get { return new Point(x, y); }
            set
            {
                x = value.x;
                y = value.y;
            }
        }

        public bool StrictlyIntersectsWith(Circle circle) => Center.GetDistance(circle.Center).IsDoubleLess(r + circle.r);

        public Line GetIntersectionLine(Circle circle) => StrictlyIntersectsWith(circle)
            ? new Segment(Center, circle.Center).MiddlePerpendicular
            : null;

        public void Transfer(Vector transferVector)
        {
            var center = Center;
            center += transferVector;
            x = center.x;
            y = center.y;
        }

        public bool StrictlyIntersectsWith(Rectangle rectangle) => rectangle.StrictlyIntersectsWith(this);

        public Line GetIntersectionLine(Rectangle rectangle) => rectangle.GetIntersectionLine(this);

        public bool Contains(Point point) => Center.GetDistance(point).IsDoubleLessEqual(r);

        public static Circle operator +(Circle circle, Vector vector) => new Circle(circle.Center + vector, circle.r);

        public static Circle operator -(Circle circle, Vector vector) => new Circle(circle.Center - vector, circle.r);
    }
}