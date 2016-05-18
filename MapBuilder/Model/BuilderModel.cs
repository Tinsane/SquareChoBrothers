using System;
using System.Drawing;
using System.IO;
using System.Linq;
using Geometry;
using SquareChoBrothers.Model;
using SquareChoBrothers.Properties;
using Newtonsoft.Json;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;
using System.Web.Script.Serialization;
using SquareChoBrothers.Model.Factories;

namespace MapBuilder.Model
{
    public class BuilderModel
    {
        public const double SquareSize = GameModel.CellSize;
        public static readonly Vector Up = new Vector(0, -SquareSize);
        public static readonly Vector Down = -Up;
        public static readonly Vector Left = new Vector(-SquareSize, 0);
        public static readonly Vector Right = -Left;
        public Square Current;
        public Action Draw, Close;
        public Map Map;
        public double Width, Height;


        public BuilderModel()
        {
            Map = new Map{Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4), 
                "background")};
            const double size = SquareSize;
            Current = new Square(new Point(size/2, size/2), size);
            Width = 1e4;
            Height = 1e4;
        }

        public Picture CurrentPicture => new Picture(Current, "me_builder_50");

        public void StartGame(Action draw, Action close)
        {
            Draw += draw;
            Close += () => save("map.txt");
            Close += close;
        }

        private void save(string fileName)
        {
            var data = JsonConvert.SerializeObject(Map);
            File.WriteAllText(fileName, data);
        }

        private bool RightCoordinates(double x, double y) =>
            x >= 0 && x <= Width && y >= 0 && y <= Height;

        private bool RightCoordinates(Square location) =>
            location.Points.All(x => RightCoordinates(x.x, x.y));

        private bool TryNewLocation(Square newLocation)
        {
            if (!RightCoordinates(newLocation))
                return false;
            Current = newLocation;
            Draw();
            return true;
        }

        public bool TryUp() => TryNewLocation(Current.GetTransfered(Up));

        public bool TryDown() => TryNewLocation(Current.GetTransfered(Down));

        public bool TryLeft() => TryNewLocation(Current.GetTransfered(Left));

        public bool TryRight() => TryNewLocation(Current.GetTransfered(Right));

        public void AddTerrain() => Map.Terrains.Add(Map.TerrainFactory.GetNext(Current));

        public void AddHero()
        {
            if (Map.Heroes.Count == 0)
                Map.Heroes.Add(Map.Hero1Factory.GetNext(Current));
            else if (Map.Heroes.Count == 1)
                Map.Heroes.Add(Map.Hero2Factory.GetNext(Current));
        }

        public void AddMonster() => Map.Monsters.Add(Map.MonsterFactory.GetNext(Current));

        public void Delete()
        {
            Map.Pictures.RemoveAll(x => x.GraphicalPosition == Current);
            Map.Heroes.RemoveAll(x => x.GraphicalPosition == Current);
            Map.Monsters.RemoveAll(x => x.GraphicalPosition == Current);
            Map.Terrains.RemoveAll(x => x.GraphicalPosition == Current);
        }
    }
}