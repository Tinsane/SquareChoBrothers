using Geometry;

namespace SquareChoBrothers.Model.Factories
{
    public class PictureFactory : MapObjectFactory<Picture, Rectangle>
    {
        private PictureFactory(string imageName) : base(imageName)
        {
        }

        public override Picture GetNext(Rectangle rectangle) => new Picture(rectangle.GetCopy(), ImageName);
    }
}