using System.Drawing;
using Newtonsoft.Json;
using SquareChoBrothers.Properties;
using SquareChoBrothers.View;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    [JsonObject(MemberSerialization.Fields)]
    public class Picture : IDrawable
    {
        public Rectangle GraphicalPosition { get; }
        public Brush Brush => brush ?? (brush = new TextureBrush((Image)Resources.ResourceManager.GetObject(ImageName)));
        [JsonIgnore]
        private Brush brush;
        public string ImageName { get; }

        public Picture(Rectangle graphicalPosition, string imageName)
        {
            GraphicalPosition = graphicalPosition.GetCopy();
            ImageName = imageName;
            brush = new TextureBrush((Image)Resources.ResourceManager.GetObject(imageName));
        }
    }
}