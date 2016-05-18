using System.Drawing;
using Newtonsoft.Json;
using SquareChoBrothers.Properties;
using SquareChoBrothers.View;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    [JsonObjectAttribute(MemberSerialization.Fields)]
    public class Picture : IDrawable
    {
        public Rectangle GraphicalPosition { get; }
        public Brush Brush => brush ?? (brush = new TextureBrush((Image)(Resources.ResourceManager.GetObject(imageName))));
        [JsonIgnore]
        private Brush brush;
        public string imageName { get; }

        public Picture(Rectangle graphicalPosition, string imageName)
        {
            GraphicalPosition = graphicalPosition.GetCopy();
            this.imageName = imageName;
            brush = new TextureBrush((Image)(Resources.ResourceManager.GetObject(imageName)));
        }
    }
}