using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public abstract class DynamicPhysicalObject<T> : PhysicalObject<T>
        where T : IGeometryFigure
    {
        private Vector velocity;

        protected DynamicPhysicalObject(Rectangle graphicalPosition, Brush brush, T hitBox) :
            this(graphicalPosition, brush, hitBox, new Vector(0, 0))
        {
        }

        protected DynamicPhysicalObject(Rectangle graphicalPosition, Brush brush, T hitBox, Vector velocity) :
            base(graphicalPosition, brush, hitBox)
        {
            Velocity = velocity;
        }

        protected Vector Velocity
        {
            get { return velocity; }
            set
            {
                if (value.Length > Physics.SpeedOfLight)
                    value.Normalize(Physics.SpeedOfLight);
                velocity = value;
            }
        }

        private void Transfer(Vector shift)
        {
            HitBox.Transfer(shift);
            GraphicalPosition.Transfer(shift);
        }

        private void PushAway(IGeometryFigure intersected, Line intersectionLine)
        {
            var shift = intersectionLine.NormalVector;
            shift /= 10;
            if (HitBox.GetTransfered(shift).IntersectsWith(intersected))
                shift = -shift;
            Transfer(shift);
        }

        private void Reflect(double dTime, List<IGeometryFigure> reflectables)
        {
            while (true)
            {
                var movedHitBox = HitBox.GetTransfered(Velocity*dTime);
                var intersected = reflectables.FirstOrDefault(reflectable =>
                !ReferenceEquals(HitBox, reflectable) && movedHitBox.IntersectsWith(reflectable));
                if (ReferenceEquals(intersected, null))
                    return;
                var previousVelocity = Velocity;
                Velocity = Velocity.GetProjection(movedHitBox.GetIntersectionLine(intersected));
                if (Velocity == previousVelocity)
                    PushAway(intersected, movedHitBox.GetIntersectionLine(intersected));
            }
        }

        public void Update(double deltaTime, List<IGeometryFigure> reflectables)
        {
            deltaTime /= TimeSpan.TicksPerMillisecond;
            const int coef = 40;
            for (var i = 0; i < coef; ++i)
            {
                var dTime = deltaTime/coef;
                lock (Velocity)
                {
                    Velocity += Physics.GravityVector*dTime;
                    Reflect(dTime, reflectables);
                    Transfer(Velocity*dTime);
                }
            }
            ((TextureBrush) Brush).ResetTransform();
            ((TextureBrush) Brush).TranslateTransform((float) GraphicalPosition.A.x,
                (float) GraphicalPosition.A.y);
        }
    }
}