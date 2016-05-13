using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geometry;
using SquareChoBrothers.Model;
using SquareChoBrothers.Model.Factories;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers
{
    public class MonsterFactory : MapObjectFactory<Monster, Square>
    {
        public MonsterFactory(Brush brush) : base(brush)
        {
        }
        
        public override Monster GetNext(Square square) => new Monster(square.GetCopy(), Brush);
    }
}
