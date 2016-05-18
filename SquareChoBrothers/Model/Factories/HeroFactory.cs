using System.Drawing;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model.Factories
{
    public class HeroFactory : MapObjectFactory<Hero, Rectangle>
    {
        private readonly double density;
        public HeroFactory(string imageName, double density) : base(imageName)
        {
            this.density = density;
        }

        public override Hero GetNext(Rectangle rectangle) => new Hero(rectangle.GetCopy(), imageName, density*rectangle.Area);
    }
}