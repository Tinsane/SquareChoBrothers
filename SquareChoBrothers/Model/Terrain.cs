using System.Drawing;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Terrain : PhysicalObject<Rectangle>
    {
        public Terrain (Rectangle graphicalPosition, Brush brush) : base(graphicalPosition, brush, graphicalPosition)
        {
        }
        public Terrain(Rectangle graphicalPosition, Brush brush, Rectangle hitBox) : base(graphicalPosition, brush, hitBox)
        {
        }
    }
}