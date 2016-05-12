using System.Drawing;
using Geometry;

namespace SquareChoBrothers.Model
{
    public class Hero : DynamicPhysicalObject<Square>
    {
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
            Velocity += new Vector(0, -2 * Physics.Impulse);
        }

        public Hero (Square graphicalPosition, Brush brush) :
            base(graphicalPosition, brush, graphicalPosition)
        {
        }
    }
}