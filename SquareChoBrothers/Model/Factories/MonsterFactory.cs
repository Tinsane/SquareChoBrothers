using Geometry;

namespace SquareChoBrothers.Model.Factories
{
    public class MonsterFactory : MapObjectFactory<Monster, Square>
    {
        private readonly double density;

        public MonsterFactory(string imageName, double density) : base(imageName)
        {
            this.density = density;
        }

        public override Monster GetNext(Square square)
            => new Monster(square.GetCopy(), ImageName, density*new Circle(square).Area);
    }
}