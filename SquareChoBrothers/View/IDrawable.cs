using System.Drawing;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.View
{
    public interface IDrawable
    {
        Rectangle GraphicalPosition { get; }
        Brush Brush { get; }
    }
}