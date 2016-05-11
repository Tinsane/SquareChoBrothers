using System.Drawing;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Terrain : PhysicalObject<Rectangle>
    {
        public Terrain(Rectangle graphicalPosition, Brush brush) : base(graphicalPosition, brush)
        {
        }

        public override Rectangle HitBox { get; set; }
    }
}