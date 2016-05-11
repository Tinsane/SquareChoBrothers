using System.Drawing;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public abstract class DynamicPhysicalObject<T> : PhysicalObject<T>
        where T : IGeometryFigure
    {
        private Vector Velocity { get; set; }

        protected DynamicPhysicalObject(Rectangle graphicalPosition, Brush brush) : base(graphicalPosition, brush)
        {
        }

        public void ChangeVelocity(Vector vector)
        {
            Velocity += vector;
        }

        public void UpdatePosition(double deltaT)
        {
            GraphicalPosition.Transfer(Velocity * deltaT);
            HitBox.Transfer(Velocity*deltaT);
        }
    }
}