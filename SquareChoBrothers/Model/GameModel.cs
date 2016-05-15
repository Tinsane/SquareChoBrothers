using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Timers;
using Geometry;
using SquareChoBrothers.Model.Factories;
using SquareChoBrothers.Properties;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;
using Timer = System.Threading.Timer;

namespace SquareChoBrothers.Model
{
    public class GameModel
    {
        private const int UpdateInterval = 2;
        public const double CellSize = 50;
        public Picture Background;

        public HeroFactory Hero1Factory = new HeroFactory(new TextureBrush(Resources.Hero1_50));

        public HeroFactory Hero2Factory = new HeroFactory(new TextureBrush(Resources.Hero2_50));
        public List<Hero> Heroes = new List<Hero>();

        public MonsterFactory MonsterFactory =
            new MonsterFactory(new TextureBrush(Resources.Monster_50));

        public List<Monster> Monsters = new List<Monster>();
        public List<Picture> Pictures = new List<Picture>();

        public TerrainFactory TerrainFactory =
            new TerrainFactory(new TextureBrush(Resources.Terrain1));

        public List<Terrain> Terrains = new List<Terrain>();

        new Timer physicsTimer;
        new Timer drawTimer;

        public GameModel()
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4),
                new TextureBrush(Resources.background));
            Terrains.Add(TerrainFactory.GetNext(new Rectangle(new Point(25, 300), 50, 600)));
            Terrains.Add(TerrainFactory.GetNext(new Rectangle(new Point(300, 625), 600, 50)));
            Terrains.Add(TerrainFactory.GetNext(new Rectangle(new Point(625, 300), 50, 600)));
            Terrains.Add(TerrainFactory.GetNext(new Rectangle(new Point(575, 300), 50, 400)));
            Terrains.Add(TerrainFactory.GetNext(new Rectangle(new Point(325, 25), 600, 50)));
            Heroes.Add(Hero1Factory.GetNext(new Square(new Point(100, 100), CellSize)));
        }

        public GameModel(List<Terrain> terrains, List<Hero> heroes,
            List<Monster> monsters, List<Picture> pictures)
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4),
                new TextureBrush(Resources.background));
            Heroes = heroes;
            Monsters = monsters;
            Terrains = terrains;
            Pictures = pictures;
        }

        public Action EndGame { get; private set; }
        private Action draw;

        // ReSharper disable once ParameterHidesMember
        public void StartGame(Action draw, Action endGame)
        {
            this.draw = draw;
            EndGame = endGame;
            physicsTimer = new Timer(UpdateState, null, UpdateInterval, Timeout.Infinite);
            //drawTimer = new Timer((state) => { draw(); drawTimer.Change(UpdateInterval, Timeout.Infinite); },
                //null, UpdateInterval, Timeout.Infinite);
        }

        private void UpdateState(object state)
        {
            var reflectables = new List<IGeometryFigure>();
            reflectables.AddRange(Heroes.Select(hero => hero.HitBox));
            reflectables.AddRange(Terrains.Select(terrain => terrain.HitBox));
            foreach (var hero in Heroes)
                hero.Update(UpdateInterval, reflectables);
            foreach (var monster in Monsters)
                monster.Update(UpdateInterval, reflectables);
            physicsTimer.Change(UpdateInterval, Timeout.Infinite);
            draw();
        }
    }
}