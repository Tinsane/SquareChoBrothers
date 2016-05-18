using System.Drawing;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Monster : DynamicPhysicalObject<Circle>
    {
        public Monster(Square graphicalPosition, string imageName)
            : base(graphicalPosition.GetCopy(), imageName, new Circle(graphicalPosition.Center, graphicalPosition.Size))
        {
            
        }
    }
}