using System.Drawing;
using Newtonsoft.Json;
using SquareChoBrothers.Properties;

namespace SquareChoBrothers.Model.Factories
{
    [JsonObject(MemberSerialization.Fields)]
    public abstract class MapObjectFactory<TObject, THitBox>
    {
        [JsonIgnore] private Brush brush;

        protected MapObjectFactory(string imageName)
        {
            ImageName = imageName;
            brush = new TextureBrush((Image) Resources.ResourceManager.GetObject(imageName));
        }

        public Brush Brush
            => brush ?? (brush = new TextureBrush((Image) Resources.ResourceManager.GetObject(ImageName)));

        public string ImageName { get; }

        public abstract TObject GetNext(THitBox hitBox);
    }
}