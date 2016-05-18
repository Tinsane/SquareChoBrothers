using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Geometry;
using Newtonsoft.Json;
using SquareChoBrothers.Model.Physics;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    [JsonObject(MemberSerialization.Fields)]
    public class Hero : DynamicPhysicalObject<Rectangle>
    {
        private const double MovementImpulse = PhysicalLaws.SpeedOfLight/5;
        private const double JumpImpulse = MovementImpulse*3;

        static Hero()
        {
            MovementImpulseVector = new Vector(MovementImpulse, 0);
            JumpImpulseVector = new Vector(0, -JumpImpulse);
        }

        public Hero(Rectangle graphicalPosition, string imageName, double mass) :
            base(graphicalPosition, imageName, graphicalPosition.GetCopy(),
                new Vector(MovementImpulse, JumpImpulse*2), mass)
        {
        }

        private static Vector MovementImpulseVector { get; }
        private static Vector JumpImpulseVector { get; }
        public bool Alive { get; set; }

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
                if (IsOnGround(map))
                    Velocity += JumpImpulseVector;
        }

        public new void Update(double deltaTime, Map map)
        {
            base.Update(deltaTime, map);
            Alive = !map.Monsters.Any(monster => monster.HitBox.IntersectsWith(HitBox));
        }

        protected override void ResolveCollisions(double dTime, Map map)
        {
            Reflect(dTime, map.Terrains);
            Reflect(dTime, map.Heroes);
        }
    }
}