using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SquareChoBrothers.Model;
using Geometry;

namespace MapBuilder.Model
{
    public class BuilderModel
    {
        public const double squareSize = 10;
        public static readonly Vector Up = new Vector(0, squareSize);
        public static readonly Vector Down = -Up;
        public static readonly Vector Left = new Vector(squareSize, 0);
        public static readonly Vector Right = -Left;
        public Action draw, Close;
        public double Width, Height;
        public List<Terrain> Terrains;
        public List<Picture> Pictures;
        public List<Hero> Heroes;
        public List<Monster> Monsters;
        public Picture Background;
        public Rectangle Current;

        public BuilderModel()
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e3, 1e3), System.Drawing.Brushes.White);
            var size = squareSize / 2;
            Current = new Rectangle(new Point(size, size), size, size);
            Width = 1e3;
            Height = 1e3;
        }

        public void StartGame(Action draw, Action close)
        {
            this.draw += draw;
            this.Close += close;
        }

        private bool RightCoordinates(double x, double y) => 
            x >= 0 && x <= Width && y >= 0 && y <= Height;

        private bool RightCoordinates(Rectangle location) =>
            location.Points.All(x => RightCoordinates(x.x, x.y));

        private bool TryNewLocation(Rectangle newLocation)
        {
            if (RightCoordinates(newLocation))
            {
                Current = newLocation;
                draw();
                return true;
            }
            return false;
        }

        public bool TryUp() => TryNewLocation(Current.GetTransfered(Up));

        public bool TryDown() => TryNewLocation(Current.GetTransfered(Up));

        public bool TryLeft() => TryNewLocation(Current.GetTransfered(Up));

        public bool TryRight() => TryNewLocation(Current.GetTransfered(Up));

        public void AddTerrain() => Terrains.Add(new Terrain())

        public void AddHero() =>
            
        public void AddMonster() =>
    }
}
