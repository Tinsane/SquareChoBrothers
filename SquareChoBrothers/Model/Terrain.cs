using System.Drawing;
using SquareChoBrothers.Model.Physics;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Terrain : PhysicalObject<Rectangle>
    {
        public Terrain (Rectangle graphicalPosition, Brush brush) :
            base(graphicalPosition, brush, graphicalPosition.GetCopy(), double.PositiveInfinity)
        {
        }
    }
}