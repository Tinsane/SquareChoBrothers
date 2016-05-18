using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquareChoBrothers.Model;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;
using SquareChoBrothers.Model.Factories;

namespace SquareChoBrothers
{
    public class TerrainFactory : MapObjectFactory<Terrain, Rectangle>
    {
        public TerrainFactory(string imageName) : base(imageName)
        {
            
        }

        public override Terrain GetNext(Rectangle rectangle) => new Terrain(rectangle.GetCopy(), imageName);
    }
}
