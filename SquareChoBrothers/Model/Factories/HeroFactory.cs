using System.Drawing;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model.Factories
{
    public class HeroFactory : MapObjectFactory<Hero, Rectangle>
    {
        private readonly double density;
        public HeroFactory(Brush brush, double density) : base(brush)
        {
            this.density = density;
        }

        public override Hero GetNext(Rectangle rectangle) => new Hero(rectangle.GetCopy(), Brush, density*rectangle.Area);
    }
}