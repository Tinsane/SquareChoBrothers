using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public class Circle : Geom, IGeometryFigure
    {
        public double x, y, r;

        public Point Center => GetCentre();

        public Circle(double x, double y, double radius)
        {
            this.x = x;
            this.y = y;
            this.r = radius;
        }

        public Circle(Point centre, double radius)
        {
            x = centre.x;
            y = centre.y;
            r = radius;
        }

        public Point GetCentre()
        {
            return new Point(x, y);
        }

        public bool Contains(Point point)
        {
            return Center.GetDistance(point) < r + precision;
        }

        public static Circle operator +(Circle circle, Vector vector)
        {
            return new Circle(circle.GetCentre() + vector, circle.r);
        }

        public static Circle operator -(Circle circle, Vector vector)
        {
            return new Circle(circle.GetCentre() - vector, circle.r);
        }

        public bool IntersectsWith(Circle circle)
        {
            return Center.GetDistance(circle.Center) < r + circle.r + precision;
        }
        
        public Line GetIntersectionLine(Circle circle)
        {
            return IntersectsWith(circle) ?
                new Segment(Center, circle.Center).GetMiddlePerpendicular() : null;
        }

        public void Transfer(Vector transferVector)
        {
            var centre = GetCentre();
            centre += transferVector;
            x = centre.x;
            y = centre.y;
        }

        public bool IntersectsWith(Square square)
        {
            return square.IntersectsWith(this);
        }

        public Line GetIntersectionLine(Square square)
        {
            return square.GetIntersectionLine(this);
        }
    }
}
