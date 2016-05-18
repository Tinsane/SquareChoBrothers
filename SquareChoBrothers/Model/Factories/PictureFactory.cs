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
    public class PictureFactory : MapObjectFactory<Picture, Rectangle>
    {
        PictureFactory(string imageName) : base(imageName)
        {
            
        }

        public override Picture GetNext(Rectangle rectangle) => new Picture(rectangle.GetCopy(), imageName);
    }
}
