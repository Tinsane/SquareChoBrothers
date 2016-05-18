using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SquareChoBrothers.Properties;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model.Factories
{
    [JsonObjectAttribute(MemberSerialization.Fields)]
    public abstract class MapObjectFactory<TObject, THitBox>
    {
        public Brush Brush => brush ?? (brush = new TextureBrush((Image) (Resources.ResourceManager.GetObject(imageName))));
        [JsonIgnore]
        private Brush brush;

        public string imageName { get; }

        public MapObjectFactory(string imageName)
        {
            this.imageName = imageName;
            brush = new TextureBrush((Image)(Resources.ResourceManager.GetObject(imageName)));
        }

        public abstract TObject GetNext(THitBox hitBox);
    }
}
