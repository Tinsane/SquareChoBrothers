using System.Drawing;
using Geometry;

namespace SquareChoBrothers.Model
{
    public class Terrain : PhysicalObject<Square>
    {
        public Terrain (Square graphicalPosition, Brush brush) : base(graphicalPosition, brush, graphicalPosition)
        {
        }
        public Terrain(Square graphicalPosition, Brush brush, Square hitBox) : base(graphicalPosition, brush, hitBox)
        {
        }
    }
}