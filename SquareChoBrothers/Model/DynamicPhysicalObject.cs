using System;
using System.Drawing;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public abstract class DynamicPhysicalObject<T> : PhysicalObject<T>
        where T : IGeometryFigure
    {
        protected DynamicPhysicalObject(Rectangle graphicalPosition, Brush brush, T hitBox) :
            base(graphicalPosition, brush, hitBox)
        {
            Velocity = new Vector(0, 0);
        }

        protected Vector Velocity { get; set; }

        public void UpdatePosition(double deltaT)
        {
            deltaT /= TimeSpan.TicksPerSecond;
            Velocity += Physics.GravityVector*deltaT;
            GraphicalPosition.Transfer(Velocity*deltaT);
            HitBox.Transfer(Velocity*deltaT);
            ((TextureBrush) Brush).ResetTransform();
            ((TextureBrush) Brush).TranslateTransform((float) GraphicalPosition.A.x,
                (float) GraphicalPosition.A.y);
        }
    }
}