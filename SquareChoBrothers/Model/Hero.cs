using System.Drawing;
using Geometry;

namespace SquareChoBrothers.Model
{
    public class Hero : DynamicPhysicalObject<Square>
    {
        public Hero (Square graphicalPosition, Brush brush) :
            base(graphicalPosition, brush, graphicalPosition)
        {
        }
        public Hero(Square graphicalPosition, Brush brush, Square hitBox) : 
            base(graphicalPosition, brush, hitBox)
        {
        }
    }
}