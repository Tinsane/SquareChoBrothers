using System.Drawing;
using Geometry;

namespace SquareChoBrothers.Model
{
    public abstract class DynamicPhysicalObject<T> : PhysicalObject<T>
        where T : IGeometryFigure
    {
        private Vector Velocity { get; set; }


        public void ChangeVelocity(Vector vector)
        {
            Velocity += vector;
        }

        public void UpdatePosition(double deltaT)
        {
            deltaT /= 1000;
            GraphicalPosition.Transfer(Velocity * deltaT);
            HitBox.Transfer(Velocity*deltaT);
        }

        protected DynamicPhysicalObject(Square graphicalPosition, Brush brush, T hitBox) : 
            base(graphicalPosition, brush, hitBox)
        {
            Velocity = new Vector(0, 0);
        }
    }
}