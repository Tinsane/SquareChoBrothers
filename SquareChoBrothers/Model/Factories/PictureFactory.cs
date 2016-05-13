using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model.Factories
{
    class PictureFactory : MapObjectFactory<Picture>
    {
        PictureFactory(Rectangle rectangle, Brush brush) : base(brush, rectangle) { }
        public override Picture GetNext(Point center) =>
            new Picture(new Rectangle(center, Rectangle.SizeX, Rectangle.SizeY), Brush);

        public override Picture GetNext(Rectangle rectangle)
        {
            throw new NotImplementedException();
        }
    }
}
