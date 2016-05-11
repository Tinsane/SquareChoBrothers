using System.Drawing;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Hero : DynamicPhysicalObject<Rectangle>
    {
        public Hero(Rectangle graphicalPosition, Brush brush) : base(graphicalPosition, brush)
        {

        }
        public override Vector Velocity { get; set; }
        public override Rectangle HitBox { get; set; }
    }
}