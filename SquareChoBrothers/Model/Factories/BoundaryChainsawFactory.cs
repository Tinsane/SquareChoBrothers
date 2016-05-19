using Geometry;

namespace SquareChoBrothers.Model.Factories
{
    public class BoundaryChainsawFactory : MapObjectFactory<BoundaryChainsaw, Square>
    {
        public BoundaryChainsawFactory(string imageName) : base(imageName)
        {
        }

        public BoundaryChainsaw GetNext (Square hitBox, Vector velocity) =>
            new BoundaryChainsaw(hitBox.GetCopy(), ImageName, 0, velocity);

        public override BoundaryChainsaw GetNext(Square hitBox)
        {
            throw new System.NotImplementedException();
        }
    }
}