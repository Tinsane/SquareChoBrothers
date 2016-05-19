using Geometry;

namespace SquareChoBrothers.Model.Factories
{
    public class HeroFactory : MapObjectFactory<Hero, Rectangle>
    {
        private readonly double density;

        public HeroFactory(string imageName, double density) : base(imageName)
        {
            this.density = density;
        }

        public override Hero GetNext(Rectangle rectangle)
            => new Hero(rectangle.GetCopy(), ImageName, density*rectangle.Area);
    }
}