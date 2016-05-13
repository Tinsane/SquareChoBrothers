using System.Drawing;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers
{
    public interface IMapObject
    {
        IMapObject(Rectangle rectangle, Brush brush);
    }
}