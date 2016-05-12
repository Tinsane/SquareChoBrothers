using System.Drawing;
using Geometry;

namespace SquareChoBrothers.Model
{
    public class Hero : DynamicPhysicalObject<Square>
    {
        private const double Impulse = 300;

        public void MoveRight()
        {
            var oyProjection = Velocity.GetScalarProduct(new Vector(0, 1));
            Velocity = new Vector(Impulse, oyProjection);
        }

        public void Stay()
        {
            var oyProjection = Velocity.GetScalarProduct(new Vector(0, 1));
            Velocity = new Vector(0, oyProjection);
        }

        public void MoveLeft()
        {
            var oyProjection = Velocity.GetScalarProduct(new Vector(0, 1));
            Velocity = new Vector(-Impulse, oyProjection);
        }

        public void Jump()
        {
            var oxProjection = Velocity.GetScalarProduct(new Vector(1, 0));
            Velocity = new Vector(oxProjection, -Impulse);
        }

        public Hero (Square graphicalPosition, Brush brush) :
            base(graphicalPosition, brush, graphicalPosition)
        {
        }
    }
}