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
        protected DynamicPhysicalObject (Rectangle graphicalPosition, Brush brush, T hitBox) :
            this(graphicalPosition, brush, hitBox, new Vector(0, 0))
        { }
        protected DynamicPhysicalObject (Rectangle graphicalPosition, Brush brush, T hitBox, Vector velocity) :
            base(graphicalPosition, brush, hitBox)
        {
            Velocity = velocity;
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

        public void Reflect(double dTime, List<IGeometryFigure> reflectables)
        {
            while (true)
            {
                var movedHitBox = HitBox.GetTransfered(Velocity * dTime);
                var velocityChanged = false;
                foreach (var reflectable in reflectables.Where(reflectable =>
                movedHitBox.IntersectsWith(reflectable) && !ReferenceEquals(HitBox, reflectable)))
                {
                    var previousVelocity = Velocity;
                    //Velocity = Velocity.GetReflected(movedHitBox.GetIntersectionLine(reflectable));
                    Velocity = Velocity.GetProjection(movedHitBox.GetIntersectionLine(reflectable));
                    velocityChanged = Velocity != previousVelocity;
                }
                if (!velocityChanged)
                    return;
            }
        }

        public void Update(double deltaTime, List<IGeometryFigure> reflectables)
        {
            deltaTime /= TimeSpan.TicksPerMillisecond;
            const int coef = 20;
            for (var i = 0; i < coef; ++i)
            {
                var dTime = deltaTime / coef;
                Velocity += Physics.GravityVector*dTime;

                Reflect(dTime, reflectables);

                GraphicalPosition.Transfer(Velocity*dTime);
                HitBox.Transfer(Velocity * dTime);
            }
            ((TextureBrush) Brush).ResetTransform();
            ((TextureBrush) Brush).TranslateTransform((float) GraphicalPosition.A.x,
                (float) GraphicalPosition.A.y);
        }
    }
}