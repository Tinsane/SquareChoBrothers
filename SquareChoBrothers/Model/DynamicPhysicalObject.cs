using System;
using System.Collections.Generic;
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

        public void Reflect(double deltaT, List<IGeometryFigure> reflectables)
        {
            while (true)
            {
                var movedHitBox = HitBox.GetTransfered(Velocity*deltaT);
                var velocityChanged = false;
                foreach (var reflectable in reflectables)
                {
                    if (reflectable is Circle)
                    {
                        var circle = reflectable as Circle;
                        if (!movedHitBox.StrictlyIntersectsWith(circle)
                            || (HitBox is Circle && HitBox as Circle == circle)) continue;
                        velocityChanged = true;
                        Velocity = Velocity.GetProjection(movedHitBox.GetIntersectionLine(circle));
                        //Velocity = Velocity.GetReflected(movedHitBox.GetIntersectionLine(circle));
                        continue;
                    }
                    var rectangle = reflectable as Rectangle;
                    if (!movedHitBox.StrictlyIntersectsWith(rectangle) ||
                        (HitBox is Rectangle && HitBox as Rectangle == rectangle)) continue;
                    velocityChanged = true;
                    Velocity = Velocity.GetProjection(movedHitBox.GetIntersectionLine(rectangle));
                    //Velocity = Velocity.GetReflected(movedHitBox.GetIntersectionLine(rectangle));
                }
                if (!velocityChanged)
                    return;
            }
        }

        public void Update(double deltaT, List<IGeometryFigure> reflectables)
        {
            deltaT /= TimeSpan.TicksPerSecond;
            const int Coef = 100;
            for (var i = 0; i < Coef; ++i)
            {
                Velocity += Physics.GravityVector*deltaT/ Coef;

                Reflect(deltaT, reflectables);

                GraphicalPosition.Transfer(Velocity*deltaT/ Coef);
                HitBox.Transfer(Velocity*deltaT/ Coef);
            }
            ((TextureBrush) Brush).ResetTransform();
            ((TextureBrush) Brush).TranslateTransform((float) GraphicalPosition.A.x,
                (float) GraphicalPosition.A.y);
        }
    }
}