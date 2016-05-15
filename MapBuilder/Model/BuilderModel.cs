using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquareChoBrothers.Model;
using Geometry;
using SquareChoBrothers;
using SquareChoBrothers.Model.Factories;
using SquareChoBrothers.View;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;
using Newtonsoft.Json;

namespace MapBuilder.Model
{
    public class BuilderModel
    {
        public const double squareSize = GameModel.CellSize;
        public static readonly Vector Up = new Vector(0, -squareSize);
        public static readonly Vector Down = -Up;
        public static readonly Vector Left = new Vector(-squareSize, 0);
        public static readonly Vector Right = -Left;
        public Action draw, Close;
        public double Width, Height;
        public Square Current;
        public Picture CurrentPicture => new Picture(Current, new TextureBrush(SquareChoBrothers.Properties.Resources.me_builder_50));
        public Map map;
        

        public BuilderModel()
        {
            map = new Map();
            map.Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4), System.Drawing.Brushes.DarkGray);
            var size = squareSize;
            Current = new Square(new Point(size / 2, size / 2), size);
            Width = 1e4;
            Height = 1e4;
        }

        public void StartGame(Action draw, Action close)
        {
            this.draw += draw;
            this.Close += close;
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
            draw();
            return true;
        }

        public bool TryUp() => TryNewLocation(Current.GetTransfered(Up));

        public bool TryDown() => TryNewLocation(Current.GetTransfered(Down));

        public bool TryLeft() => TryNewLocation(Current.GetTransfered(Left));

        public bool TryRight() => TryNewLocation(Current.GetTransfered(Right));

        public void AddTerrain() => map.Terrains.Add(map.TerrainFactory.GetNext(Current));

        public void AddHero()
        {
            if (map.Heroes.Count == 0)
                map.Heroes.Add(map.Hero1Factory.GetNext(Current));
            else if (map.Heroes.Count == 1)
                map.Heroes.Add(map.Hero2Factory.GetNext(Current));
        }

        public void AddMonster() => map.Monsters.Add(map.MonsterFactory.GetNext(Current));

        public void Delete()
        {
            map.Pictures.RemoveAll(x => x.GraphicalPosition == Current);
            map.Heroes.RemoveAll(x => x.GraphicalPosition == Current);
            map.Monsters.RemoveAll(x => x.GraphicalPosition == Current);
            map.Terrains.RemoveAll(x => x.GraphicalPosition == Current);
        }
    }
}
