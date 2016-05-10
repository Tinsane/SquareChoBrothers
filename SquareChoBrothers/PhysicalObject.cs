using System.Drawing;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers
{
    public abstract class PhysicalObject : IRectangleDrawable
    {
        public Rectangle Position { get; }
        public Brush Brush { get; }
        public PhysicalObject(Rectangle position, Brush brush)
        {
            Position = position;
            Brush = brush;
        }
    }
}