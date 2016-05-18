using System.Drawing;

namespace SquareChoBrothers.Model.Factories
{
    public abstract class MapObjectFactory<TObject, THitBox>
    {
        protected MapObjectFactory(Brush brush)
        {
            Brush = brush;
        }

        protected Brush Brush { get; set; }

        public abstract TObject GetNext(THitBox hitBox);
    }
}