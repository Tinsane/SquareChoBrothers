using Geometry;

namespace SquareChoBrothers.Model.Factories
{
    public class TerrainFactory : MapObjectFactory<Terrain, Rectangle>
    {
        public TerrainFactory(string imageName) : base(imageName)
        {
        }

        public override Terrain GetNext(Rectangle rectangle) => new Terrain(rectangle.GetCopy(), ImageName);
    }
}