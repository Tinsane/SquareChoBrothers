using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Resources;
using SquareChoBrothers.Properties;

namespace SquareChoBrothers.Json
{
    class TextureBrushConverter : JsonConverter
    {
        static Dictionary<string, Image> images = new Dictionary<string, Image>
        {
            {"Background", Resources.background },
            {"Hero1", Resources.Hero1_50 },
            {"Hero2", Resources.Hero2_50 },
            {"Monster", Resources.Monster_50 },
            {"Terrain_red_meat", Resources.Terrain1 }
        };

        static Dictionary<Image, string> strings = images.ToDictionary(pair => pair.Value, pair => pair.Key);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var brush = (TextureBrush)value;
            writer.WriteValue(strings[brush.Image]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value.ToString();
            return new TextureBrush(images[value]);
        }

        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(TextureBrush))
                return true;
            return false;
        }
    }
}
