﻿using System;
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
                    if (!movedHitBox.StrictlyIntersectsWith(reflectable) ||
                        (HitBox is Rectangle && HitBox as Rectangle == reflectable)) continue;
                    velocityChanged = true;
                    Velocity = Velocity.GetReflected(movedHitBox.GetIntersectionLine(reflectable));
                }
                if (!velocityChanged)
                    return;
            }
        }

        public void Update(double deltaT, List<IGeometryFigure> reflectables)
        {
            deltaT /= TimeSpan.TicksPerSecond;
            Velocity += Physics.GravityVector*deltaT;

            Reflect(deltaT, reflectables);

            GraphicalPosition.Transfer(Velocity*deltaT);
            HitBox.Transfer(Velocity*deltaT);
            ((TextureBrush) Brush).ResetTransform();
            ((TextureBrush) Brush).TranslateTransform((float) GraphicalPosition.A.x,
                (float) GraphicalPosition.A.y);
        }
    }
}