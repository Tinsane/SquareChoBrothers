using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Geometry;
using SquareChoBrothers.Model.Physics;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Hero : DynamicPhysicalObject<Rectangle>
    {
        private const double MovementImpulse = PhysicalLaws.SpeedOfLight/5;
        private const double JumpImpulse = MovementImpulse*3;

        public Hero(Rectangle graphicalPosition, string imageName) :
            base(graphicalPosition, imageName, graphicalPosition.GetCopy())
        {
            IsAlive = true;
        }

        static Hero()
        {
            MovementImpulseVector = new Vector(MovementImpulse, 0);
            JumpImpulseVector = new Vector(0, -JumpImpulse);
        }

        private static Vector MovementImpulseVector { get; }
        private static Vector JumpImpulseVector { get; }
        public bool IsAlive { get; private set; }

        public void MoveRight()
        {
            lock (this)
            {
                Velocity = MovementImpulseVector + Velocity.OyProjection;
            }
        }

        public void Stay()
        {
            lock (this)
            {
                Velocity = Velocity.OyProjection;
            }
        }

        public void MoveLeft()
        {
            lock (this)
            {
                Velocity = -MovementImpulseVector + Velocity.OyProjection;
            }
        }

        public void Jump(Map map)
        {
            lock (this)
            {
                if (!IsOnGround(map))
                    return;
                Velocity += JumpImpulseVector;
            }
        }

        public void Update(double deltaTime, List<IGeometryFigure> reflectables, List<Monster> enemies)
        {
            Update(deltaTime, reflectables);
            IsAlive = !enemies.Any(enemy => enemy.HitBox.IntersectsWith(HitBox));
        }
    }
}