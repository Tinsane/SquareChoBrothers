using System.Collections.Generic;
using System.Linq;
using Geometry;
using Newtonsoft.Json;
using SquareChoBrothers.Model.Factories;
using SquareChoBrothers.Model.Physics;
using SquareChoBrothers.View;

namespace SquareChoBrothers.Model
{
    [JsonObject(MemberSerialization.Fields)]
    public class Map
    {
        public const double CellSize = GameModel.CellSize;

        public readonly HeroFactory Hero1Factory = new HeroFactory("Hero1_50", 1);

        public readonly HeroFactory Hero2Factory = new HeroFactory("Hero2_50", 1);

        public readonly MonsterFactory MonsterFactory =
            new MonsterFactory("Monster_50", 1);

        public readonly TerrainFactory TerrainFactory =
            new TerrainFactory("Terrain1");

        public readonly BoundaryChainsawFactory BoundaryChainsawFactory =
        new BoundaryChainsawFactory("Monster_50");

        public Picture Background;

        public List<Hero> Heroes;

        public List<Monster> Monsters;
        public List<Picture> Pictures;

        public List<Terrain> Terrains;
        public List<BoundaryChainsaw> BoundaryChainsaws;

        public Map()
        {
            Heroes = new List<Hero>();
            Monsters = new List<Monster>();
            Pictures = new List<Picture>();
            Terrains = new List<Terrain>();
            BoundaryChainsaws = new List<BoundaryChainsaw>
            {
                BoundaryChainsawFactory.GetNext(new Square(new Point(-200, 344), 1e3), new Vector(PhysicalLaws.SpeedOfLight / 300, 0))
            };
        }

        public Map(List<Terrain> terrains, List<Hero> heroes,
            List<Monster> monsters, List<Picture> pictures)
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4), "background");
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
                .Concat(Terrains)
            .Concat(BoundaryChainsaws);

        public IEnumerable<IGeometryFigure> HeroReflectables =>
            Heroes.Select(hero => hero.HitBox)
                .Concat(Terrains.Select(terrain => terrain.HitBox));

        public IEnumerable<DynamicPhysicalObject<Circle>> Enemies =>
            Monsters.Select(monster => (DynamicPhysicalObject<Circle>) monster)
            .Concat(BoundaryChainsaws);
    }
}