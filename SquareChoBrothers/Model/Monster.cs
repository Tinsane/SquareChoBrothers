using System.Drawing;
using Geometry;

namespace SquareChoBrothers.Model
{
    public class Monster : DynamicPhysicalObject<Circle>
    {
        public Monster(Square graphicalPosition, Brush brush, Circle hitBox) :
            base(graphicalPosition, brush, hitBox)
        {
        }
    }
}