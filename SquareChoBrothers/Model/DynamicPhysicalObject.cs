using System;
using System.Drawing;
using Geometry;

namespace SquareChoBrothers.Model
{
    public abstract class DynamicPhysicalObject<T> : PhysicalObject<T>
        where T : IGeometryFigure
    {
        protected Vector Velocity { get; set; }

        public void UpdatePosition(double deltaT)
        {
            deltaT /= TimeSpan.TicksPerSecond;
            GraphicalPosition.Transfer(Velocity*deltaT);
            HitBox.Transfer(Velocity*deltaT);
            (Brush as TextureBrush).ResetTransform();
            (Brush as TextureBrush).TranslateTransform((float) GraphicalPosition.A.x,
                (float)GraphicalPosition.A.y);
        }

        protected DynamicPhysicalObject(Square graphicalPosition, Brush brush, T hitBox) :
            base(graphicalPosition, brush, hitBox)
        {
            Velocity = new Vector(0, 0);
        }
    }
}