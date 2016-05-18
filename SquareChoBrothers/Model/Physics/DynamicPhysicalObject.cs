using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model.Physics
{
    public abstract class DynamicPhysicalObject<T> : PhysicalObject<T>
        where T : IGeometryFigure
    {
        private Vector velocity;

        protected DynamicPhysicalObject(Rectangle graphicalPosition, Brush brush, T hitBox, double mass) :
            this(graphicalPosition, brush, hitBox, new Vector(0, 0), mass)
        {
        }

        protected DynamicPhysicalObject(Rectangle graphicalPosition, Brush brush, T hitBox, Vector velocity, double mass)
            :
                base(graphicalPosition, brush, hitBox)
        {
            Velocity = velocity;
            Mass = mass;
        }

        protected double Mass { get; set; }

        public Vector Impulse
        {
            get { return Velocity*Mass; }
            protected set { Velocity = value / Mass; }
        }

        protected Vector Velocity
        {
            get { return velocity; }
            set
            {
                if (value.Length > PhysicalLaws.SpeedOfLight)
                    value.Normalize(PhysicalLaws.SpeedOfLight);
                velocity = value;
            }
        }

        protected bool IsOnGround(Map map)
        {
            var loweredHitBox = HitBox.GetTransfered(new Vector(0, 0.2));
            return map.HeroReflectables.Any(reflectable =>
                !ReferenceEquals(reflectable, HitBox) && reflectable.IntersectsWith(loweredHitBox));
        }

        private void Transfer(Vector shift)
        {
            HitBox.Transfer(shift);
            GraphicalPosition.Transfer(shift);
        }

        private void TerrainReflect(double dTime, Rectangle[] terrains)
        {
            for (var i = 0; i < 2; ++i)
            {
                var movedHitBox = HitBox.GetTransfered(Velocity*dTime);
                foreach (var intersected in terrains)
                {
                    var intersectionLine = movedHitBox.GetIntersectionLine(intersected);
                    if (ReferenceEquals(intersected, HitBox) || ReferenceEquals(intersectionLine, null))
                        continue;
                    var projection = Velocity.GetProjection(intersectionLine);
                    var normal = Velocity - projection;
                    if (normal.GetScalarProduct(intersected.Center - HitBox.Center).IsDoubleLess(0))
                        Velocity = projection + normal;
                    else
                        Velocity = projection;
                }
            }
        }

        public void Update(double deltaTime, List<Terrain> terrains)
        {
            lock (this)
            {
                deltaTime /= TimeSpan.TicksPerMillisecond;
                const int coef = 10;
                var dTime = deltaTime/coef;
                for (var i = 0; i < coef; ++i)
                {
                    Velocity += PhysicalLaws.GravityVector*dTime;
                    TerrainReflect(dTime, terrains.Select(reflectable => reflectable.HitBox).ToArray());
                    Transfer(Velocity*dTime);
                }
                ((TextureBrush) Brush).ResetTransform();
                ((TextureBrush) Brush).TranslateTransform((float) GraphicalPosition.A.x,
                    (float) GraphicalPosition.A.y);
            }
        }
    }
}