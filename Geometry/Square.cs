namespace Geometry
{
    public class Square : Rectangle
    {
        public Square(Point center, double size) : base(center, size, size)
        {
        }

        public double Size => Width;
    }
}