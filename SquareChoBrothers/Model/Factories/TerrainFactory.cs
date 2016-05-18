using System.Drawing;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model.Factories
{
    public class TerrainFactory : MapObjectFactory<Terrain, Rectangle>
    {
        public TerrainFactory(Brush brush) : base(brush)
        {
        }

        public override Terrain GetNext(Rectangle rectangle) => new Terrain(rectangle.GetCopy(), Brush);
    }
}