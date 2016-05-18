using System.Drawing;
using SquareChoBrothers.Model.Physics;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Terrain : PhysicalObject<Rectangle>
    {
        public Terrain (Rectangle graphicalPosition, string imageName) : 
            base(graphicalPosition, imageName, graphicalPosition.GetCopy())
        {
        }
    }
}