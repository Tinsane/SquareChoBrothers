using Geometry;
using SquareChoBrothers.Model.Physics;

namespace SquareChoBrothers.Model
{
    public class Terrain : PhysicalObject<Rectangle>
    {
        public Terrain(Rectangle graphicalPosition, string imageName) :
            base(graphicalPosition, imageName, graphicalPosition.GetCopy())
        {
        }
    }
}