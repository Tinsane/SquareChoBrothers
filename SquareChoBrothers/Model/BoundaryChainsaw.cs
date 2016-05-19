using Geometry;
using SquareChoBrothers.Model.Physics;

namespace SquareChoBrothers.Model
{
    public class BoundaryChainsaw : DynamicPhysicalObject<Circle>
    {
        public BoundaryChainsaw(Square graphicalPosition, string imageName, double mass, Vector velocity) :
            base(graphicalPosition.GetCopy(), imageName, new Circle(graphicalPosition), velocity, mass)
        {
        }

        public BoundaryChainsaw(Square graphicalPosition, string imageName, Circle hitBox, Vector velocity, double mass) :
            base(graphicalPosition, imageName, hitBox, velocity, mass)
        {
        }

        protected override void ResolveCollisions(double dTime, Map map)
        {
            Velocity = Velocity.OxProjection;
        }
    }
}