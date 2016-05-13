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
    public class TerrainFactory : MapObjectFactory<Terrain>
    {
        public TerrainFactory(Brush brush, Rectangle rectangle) : base(brush, rectangle) { }

        public override Terrain GetNext(Point center) =>
            new Terrain(new Rectangle(center, Rectangle.SizeX, Rectangle.SizeY), Brush);

        public override Terrain GetNext(Rectangle rectangle) => new Terrain(rectangle, Brush);
    }
}
