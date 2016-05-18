using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Geometry;
using Newtonsoft.Json;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model.Physics
{
    [JsonObject(MemberSerialization.Fields)]
    public abstract class DynamicPhysicalObject<T> : PhysicalObject<T>
        where T : IGeometryFigure
    {
        private Vector velocity;

        protected DynamicPhysicalObject(Rectangle graphicalPosition, string imageName, T hitBox, double mass) :
            this(graphicalPosition, imageName, hitBox, new Vector(0, 0), mass)
        {
        }

        protected DynamicPhysicalObject(Rectangle graphicalPosition, string imageName, T hitBox, Vector velocity, double mass)
            :
                base(graphicalPosition, imageName, hitBox)
        {
            Velocity = velocity;
            Mass = mass;
        }

        protected double Mass { get; set; }

        public Vector Impulse
        {
            get { return Velocity*Mass; }
            protected set { Velocity = value/Mass; }
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

        protected abstract void ResolveCollisions(double dTime, Map map);

        private void Transfer(Vector shift)
        {
            HitBox.Transfer(shift);
            GraphicalPosition.Transfer(shift);
        }

        private void ResolveCollision<THitBox>(PhysicalObject<THitBox> intersected, Line intersectionLine)
            where THitBox : IGeometryFigure
        {
            var dynamicPhysicalObjectIntersected = intersected as DynamicPhysicalObject<THitBox>;
            if (!ReferenceEquals(dynamicPhysicalObjectIntersected, null))
            {
                ResolveCollision(dynamicPhysicalObjectIntersected, intersectionLine);
                return;
            }
            var projection = Velocity.GetProjection(intersectionLine);
            var normal = Velocity - projection;
            if (normal.GetScalarProduct(intersected.HitBox.Center - HitBox.Center).IsDoubleLess(0))
                Velocity = projection + normal;
            else
                Velocity = projection;
        }

        private void ResolveCollision<THitBox>(DynamicPhysicalObject<THitBox> intersected, Line intersectionLine)
            where THitBox : IGeometryFigure
        {
            var thisProjection = Velocity.GetProjection(intersectionLine);
            var thisNormal = Velocity - thisProjection;
            var thatProjection = intersected.Velocity.GetProjection(intersectionLine);
            var thatNormal = intersected.Velocity - thatProjection;
            var sumNormalImpulse = thisNormal.Length*Mass + thatNormal.Length*intersected.Mass;
            var newSumNormalImpulse = sumNormalImpulse * PhysicalLaws.ImpactConstant;
            var newNormalVelocity = newSumNormalImpulse / (Mass + intersected.Mass);
            var newThisNormal = HitBox.Center.GetHeight(intersectionLine).Reversed.GetNormalized(newNormalVelocity);
            var newThatNormal = -newThisNormal;
            Velocity = thisProjection + newThisNormal;
            intersected.Velocity = thatProjection + newThatNormal;
        }

        protected void Reflect<THitBox>(double dTime, IEnumerable<PhysicalObject<THitBox>> reflectables)
            where THitBox : IGeometryFigure
        {
            var movedHitBox = HitBox.GetTransfered(Velocity*dTime);
            foreach (var reflectable in reflectables)
            {
                var intersectionLine = movedHitBox.GetIntersectionLine(reflectable.HitBox);
                if (ReferenceEquals(reflectable, this) || ReferenceEquals(intersectionLine, null))
                    continue;
                ResolveCollision(reflectable, intersectionLine);
            }
        }

        public void Update(double deltaTime, Map map)
        {
            lock (this)
            {
                deltaTime /= TimeSpan.TicksPerMillisecond;
                const int coef = 10;
                var dTime = deltaTime/coef;
                for (var i = 0; i < coef; ++i)
                {
                    Velocity += PhysicalLaws.GravityVector*dTime;
                    ResolveCollisions(dTime, map);
                    Transfer(Velocity*dTime);
                }
                var textureBrush = (TextureBrush) Brush;
                textureBrush.ResetTransform();
                textureBrush.TranslateTransform((float) GraphicalPosition.A.x,
                    (float) GraphicalPosition.A.y);
            }
        }
    }
}