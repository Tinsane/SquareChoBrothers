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

namespace MapBuilder.Model
{
    public class BuilderModel
    {
        public const double squareSize = 50;
        public static readonly Vector Up = new Vector(0, -squareSize);
        public static readonly Vector Down = -Up;
        public static readonly Vector Left = new Vector(-squareSize, 0);
        public static readonly Vector Right = -Left;
        public Action draw, Close;
        public double Width, Height;
        public List<Terrain> Terrains = new List<Terrain>();
        public List<Picture> Pictures = new List<Picture>();
        public List<Hero> Heroes = new List<Hero>();
        public List<Monster> Monsters = new List<Monster>();
        public Picture Background;
        public Square Current;
        public Picture CurrentPicture => new Picture(Current, new TextureBrush(SquareChoBrothers.Properties.Resources.me_builder_50));

        public IEnumerable<IDrawable> AllDrawables
            =>
                (new[] { Background }).Select(x => (IDrawable) x)
                    .Concat(Pictures)
                    .Concat(Heroes)
                    .Concat(Monsters)
                    .Concat(Terrains)
                    .Concat(new [] {CurrentPicture});

        public MonsterFactory monsterFactory =
            new MonsterFactory(new TextureBrush(SquareChoBrothers.Properties.Resources.Monster_50));

        public TerrainFactory terrainFactory =
            new TerrainFactory(new TextureBrush(SquareChoBrothers.Properties.Resources.Terrain1));

        public HeroFactory hero1Factory = new HeroFactory(new TextureBrush(SquareChoBrothers.Properties.Resources.Hero1_50));

        public HeroFactory hero2Factory = new HeroFactory(new TextureBrush(SquareChoBrothers.Properties.Resources.Hero2_50));

        public BuilderModel()
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4), System.Drawing.Brushes.DarkGray);
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

        public void AddTerrain() => Terrains.Add(terrainFactory.GetNext(Current));

        public void AddHero()
        {
            if (Heroes.Count == 0)
                Heroes.Add(hero1Factory.GetNext(Current));
            else if (Heroes.Count == 1)
                Heroes.Add(hero2Factory.GetNext(Current));
        }

        public void AddMonster() => Monsters.Add(monsterFactory.GetNext(Current));
    }
}
