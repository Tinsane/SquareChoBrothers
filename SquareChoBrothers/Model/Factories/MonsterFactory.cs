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

namespace SquareChoBrothers.Model.Factories
{
    public class MonsterFactory : MapObjectFactory<Monster, Square>
    {
        private readonly double density;
        public MonsterFactory(string imageName, double density) : base(imageName)
        {
            this.density = density;
        }
        
        public override Monster GetNext(Square square) => new Monster(square.GetCopy(), imageName, density * new Circle(square).Area);
    }
}
