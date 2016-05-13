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
    public abstract class MapObjectFactory<T>
    {
        protected Brush Brush { get; set; }
        protected Rectangle Rectangle { get; set; }

        public MapObjectFactory(Brush brush, Rectangle rectangle)
        {
            this.Brush = brush;
            this.Rectangle = rectangle;
        }

        public abstract T GetNext(Point center);

        public abstract T GetNext(Rectangle rectangle);
    }
}
