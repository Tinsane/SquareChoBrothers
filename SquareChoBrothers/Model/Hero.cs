using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Hero : DynamicPhysicalObject<Rectangle>
    {
        public bool IsAlive { get; private set; }

        public Hero(Rectangle graphicalPosition, Brush brush) :
            base(graphicalPosition, brush, graphicalPosition.GetCopy(),
                new Vector(Physics.SpeedOfLight / 10, Physics.SpeedOfLight))
        {
            IsAlive = true;
        }

        public void MoveRight()
        {
            lock (this)
            {
                var oyProjection = Velocity.GetScalarProduct(new Vector(0, 1));
                Velocity = new Vector(Physics.Impulse, oyProjection);
            }
        }

        public void Stay()
        {
            lock (this)
            {
                var oyProjection = Velocity.GetScalarProduct(new Vector(0, 1));
                Velocity = new Vector(0, oyProjection);
            }
        }

        public void MoveLeft()
        {
            lock (this)
            {
                var oyProjection = Velocity.GetScalarProduct(new Vector(0, 1));
                Velocity = new Vector(-Physics.Impulse, oyProjection);
            }
        }

        public void Jump(Map map)
        {
            lock (this)
            {
                if (!IsOnGround(map))
                    return;
                Velocity += new Vector(0, -3*Physics.Impulse);
            }
        }

        public void Update(double deltaTime, List<IGeometryFigure> reflectables, List<Monster> enemies)
        {
            Update(deltaTime, reflectables);
            IsAlive = !enemies.Any(enemy => enemy.HitBox.IntersectsWith(HitBox));
        }
    }
}
