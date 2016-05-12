using System.Drawing;
using SquareChoBrothers.View;
using Geometry;

namespace SquareChoBrothers.Model
{
    public class Picture : IDrawable
    {
        public Square GraphicalPosition { get; }
        public Brush Brush { get; }
        public Picture(Square graphicalPosition, Brush brush)
        {
            GraphicalPosition = graphicalPosition;
            Brush = brush;
        }
    }
}