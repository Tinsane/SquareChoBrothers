using System.Drawing;
using Geometry;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Monster : DynamicPhysicalObject<Circle>
    {
        public Monster(Square graphicalPosition, Brush brush)
            : base(graphicalPosition.GetCopy(), brush, new Circle(graphicalPosition.Center, graphicalPosition.Size))
        {
            
        }
    }
}