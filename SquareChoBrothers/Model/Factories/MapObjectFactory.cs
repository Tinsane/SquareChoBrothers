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
    public abstract class MapObjectFactory<TObject, THitBox>
    {
        protected Brush Brush { get; set; }

        public MapObjectFactory(Brush brush)
        {
            this.Brush = brush;
        }

        public abstract TObject GetNext(THitBox hitBox);
    }
}
