using System.Drawing;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers
{
    public interface IRectangleDrawable
    {
        Rectangle Position { get; }
        Brush Brush { get; }
    }
}