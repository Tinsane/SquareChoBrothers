using System.Drawing;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers
{
    public class Picture : IRectangleDrawable
    {
        public Rectangle Position { get; }
        public Brush Brush { get; }

        public Picture(Rectangle position, Brush brush)
        {
            Position = position;
            Brush = brush;
        }
    }
}