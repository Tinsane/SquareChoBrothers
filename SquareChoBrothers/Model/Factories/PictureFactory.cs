using System.Drawing;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model.Factories
{
    public class PictureFactory : MapObjectFactory<Picture, Rectangle>
    {
        private PictureFactory(Brush brush) : base(brush)
        {
        }

        public override Picture GetNext(Rectangle rectangle) => new Picture(rectangle.GetCopy(), Brush);
    }
}