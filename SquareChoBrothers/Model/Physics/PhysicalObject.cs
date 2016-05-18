using System.Drawing;
using Geometry;
using SquareChoBrothers.View;
using Rectangle = Geometry.Rectangle;
using Newtonsoft.Json;
using System.Resources;
using SquareChoBrothers.Properties;

namespace SquareChoBrothers.Model.Physics
{
    [JsonObjectAttribute(MemberSerialization.Fields)]
    public abstract class PhysicalObject<T> : IDrawable
        where T : IGeometryFigure
    {
        protected PhysicalObject(Rectangle graphicalPosition, string imageName, T hitBox)
        {
            HitBox = hitBox;
            GraphicalPosition = graphicalPosition;
            this.imageName = imageName;
            brush = new TextureBrush((Image) (Resources.ResourceManager.GetObject(imageName)));
        }

        public T HitBox { get; protected set; }
        public Rectangle GraphicalPosition { get; set; }
        [JsonIgnore]
        private Brush brush;
        public Brush Brush => brush ?? (brush = new TextureBrush((Image)(Resources.ResourceManager.GetObject(imageName))));
        public string imageName { get; }
    }
}