using System.Drawing;
using SquareChoBrothers.View;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Picture : IDrawable
    {
        public Rectangle GraphicalPosition { get; }
        public Brush Brush { get; }
        public Picture(Rectangle graphicalPosition, Brush brush)
        {
            GraphicalPosition = graphicalPosition.GetCopy();
            Brush = brush;
        }
    }
}