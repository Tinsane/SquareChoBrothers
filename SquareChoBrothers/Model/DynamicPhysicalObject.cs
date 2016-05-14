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
        protected DynamicPhysicalObject(Rectangle graphicalPosition, Brush brush, T hitBox) :
            base(graphicalPosition, brush, hitBox)
        {
            Velocity = new Vector(0, 0);
        }

        private Vector velocity;

        protected Vector Velocity {
            get
            {
                return velocity;
            }
            set
            {
                if (value.Length > Physics.SpeedOfLight)
                    value.Normalize(Physics.SpeedOfLight);
                velocity = value;
            }
        }

        public void Reflect(double deltaT, List<IGeometryFigure> reflectables)
        {
            while (true)
            {
                var movedHitBox = HitBox.GetTransfered(Velocity*deltaT);
                var velocityChanged = false;
                foreach (var reflectable in reflectables.Where(reflectable =>
                movedHitBox.StrictlyIntersectsWith(reflectable) && !ReferenceEquals(HitBox, reflectable)))
                {
                    velocityChanged = true;
                    //Velocity = Velocity.GetReflected(movedHitBox.GetIntersectionLine(reflectable));
                    Velocity = Velocity.GetProjection(movedHitBox.GetIntersectionLine(reflectable));
                }
                if (!velocityChanged)
                    return;
            }
        }

        public void Update(double deltaT, List<IGeometryFigure> reflectables)
        {
            deltaT /= TimeSpan.TicksPerSecond;
            const int coef = 100;
            for (var i = 0; i < coef; ++i)
            {
                Velocity += Physics.GravityVector*deltaT/ coef;

                Reflect(deltaT, reflectables);

                GraphicalPosition.Transfer(Velocity*deltaT/ coef);
                HitBox.Transfer(Velocity*deltaT/ coef);
            }
            ((TextureBrush) Brush).ResetTransform();
            ((TextureBrush) Brush).TranslateTransform((float) GraphicalPosition.A.x,
                (float) GraphicalPosition.A.y);
        }
    }
}