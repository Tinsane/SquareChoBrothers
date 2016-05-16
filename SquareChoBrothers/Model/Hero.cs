using System.Drawing;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Hero : DynamicPhysicalObject<Rectangle>
    {
        public Hero(Rectangle graphicalPosition, Brush brush) :
            base(graphicalPosition, brush, graphicalPosition.GetCopy(),
                new Vector(Physics.SpeedOfLight/10, Physics.SpeedOfLight))
        {
        }

        public void MoveRight()
        {
            var oyProjection = Velocity.GetScalarProduct(new Vector(0, 1));
            Velocity = new Vector(Physics.Impulse, oyProjection);
        }

        public void Stay()
        {
            var oyProjection = Velocity.GetScalarProduct(new Vector(0, 1));
            Velocity = new Vector(0, oyProjection);
        }

        public void MoveLeft()
        {
            var oyProjection = Velocity.GetScalarProduct(new Vector(0, 1));
            Velocity = new Vector(-Physics.Impulse, oyProjection);
        }

        public void Jump()
        {
            Velocity += new Vector(0, -2*Physics.Impulse);
        }
    }
}