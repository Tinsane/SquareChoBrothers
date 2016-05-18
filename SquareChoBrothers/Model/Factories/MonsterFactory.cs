using System.Drawing;
using Geometry;

namespace SquareChoBrothers.Model.Factories
{
    public class MonsterFactory : MapObjectFactory<Monster, Square>
    {
        private readonly double density;
        public MonsterFactory(Brush brush, double density) : base(brush)
        {
            this.density = density;
        }

        public override Monster GetNext(Square square) => new Monster(square.GetCopy(), Brush, density*new Circle(square).Area);
    }
}