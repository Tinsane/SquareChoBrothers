using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Geometry;
using SquareChoBrothers.Model.Factories;
using SquareChoBrothers.Properties;
using SquareChoBrothers.View;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class Map
    {
        public const double CellSize = GameModel.CellSize;

        public readonly HeroFactory Hero1Factory = new HeroFactory(new TextureBrush(Resources.Hero1_50));

        public readonly HeroFactory Hero2Factory = new HeroFactory(new TextureBrush(Resources.Hero2_50));

        public readonly MonsterFactory MonsterFactory =
            new MonsterFactory(new TextureBrush(Resources.Monster_50));

        public readonly TerrainFactory TerrainFactory =
            new TerrainFactory(new TextureBrush(Resources.Terrain1));

        public Picture Background;

        public List<Hero> Heroes;

        public List<Monster> Monsters;
        public List<Picture> Pictures;

        public List<Terrain> Terrains;

        public Map()
        {
            Heroes = new List<Hero>();
            Monsters = new List<Monster>();
            Pictures = new List<Picture>();
            Terrains = new List<Terrain>();
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4),
                new TextureBrush(Resources.background));
            Terrains.Add(TerrainFactory.GetNext(new Rectangle(new Point(25, 300), 50, 600)));
            Terrains.Add(TerrainFactory.GetNext(new Rectangle(new Point(300, 625), 600, 50)));
            Terrains.Add(TerrainFactory.GetNext(new Rectangle(new Point(625, 300), 50, 600)));
            Terrains.Add(TerrainFactory.GetNext(new Rectangle(new Point(575, 300), 50, 400)));
            Terrains.Add(TerrainFactory.GetNext(new Rectangle(new Point(325, 25), 600, 50)));
            Heroes.Add(Hero1Factory.GetNext(new Square(new Point(125, 125), CellSize)));
            Heroes.Add(Hero2Factory.GetNext(new Square(new Point(225, 225), CellSize)));
            Monsters.Add(MonsterFactory.GetNext(new Square(new Point(400, 500), CellSize)));
        }

        public Map(List<Terrain> terrains, List<Hero> heroes,
            List<Monster> monsters, List<Picture> pictures)
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4),
                new TextureBrush(Resources.background));
            Heroes = heroes;
            Monsters = monsters;
            Terrains = terrains;
            Pictures = pictures;
        }

        public IEnumerable<IDrawable> Drawables =>
            new[] {Background}.Select(x => (IDrawable) x)
                .Concat(Pictures)
                .Concat(Heroes)
                .Concat(Monsters)
                .Concat(Terrains);

        public IEnumerable<IGeometryFigure> HeroReflectables =>
            Heroes.Select(hero => hero.HitBox)
                .Concat(Terrains.Select(terrain => terrain.HitBox));
    }
}