using System.Drawing;
using Geometry;

namespace SquareChoBrothers.View
{
    public interface IDrawable
    {
        Square GraphicalPosition { get; }
        Brush Brush { get; }
    }
}