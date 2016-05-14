using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;
using Geometry;
using System.Linq;
using SquareChoBrothers.Model.Factories;
using Newtonsoft.Json;

namespace SquareChoBrothers.Model
{
    public class GameModel
    {
        private const double UpdateInterval = 5;
        public const double CellSize = 50;
        public Action EndGame { get; private set; }
        private Action draw;
        public Picture Background;
        public List<Picture> Pictures = new List<Picture>();
        public List<Hero> Heroes = new List<Hero>();
        public List<Monster> Monsters = new List<Monster>();
        public List<Terrain> Terrains = new List<Terrain>();
        private double previousSignalTime;

        public MonsterFactory monsterFactory =
            new MonsterFactory(new TextureBrush(SquareChoBrothers.Properties.Resources.Monster_50));

        public TerrainFactory terrainFactory =
            new TerrainFactory(new TextureBrush(SquareChoBrothers.Properties.Resources.Terrain1));

        public HeroFactory hero1Factory = new HeroFactory(new TextureBrush(SquareChoBrothers.Properties.Resources.Hero1_50));

        public HeroFactory hero2Factory = new HeroFactory(new TextureBrush(SquareChoBrothers.Properties.Resources.Hero2_50));

        public GameModel()
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4),
                new TextureBrush(SquareChoBrothers.Properties.Resources.background));
            Terrains.Add(terrainFactory.GetNext(new Rectangle(new Point(25, 300), 50, 600)));
            Terrains.Add(terrainFactory.GetNext(new Rectangle(new Point(300, 625), 600, 50)));
            Terrains.Add(terrainFactory.GetNext(new Rectangle(new Point(625, 300), 50, 600)));
            Terrains.Add(terrainFactory.GetNext(new Rectangle(new Point(325, 25), 600, 50)));
            Heroes.Add(hero1Factory.GetNext(new Square(new Point(100, 100), CellSize)));
        }

        public GameModel(List<Terrain> terrains, List<Hero> heroes,
            List<Monster> monsters, List<Picture> pictures)
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4), 
                new TextureBrush(SquareChoBrothers.Properties.Resources.background));
            Heroes = heroes;
            Monsters = monsters;
            Terrains = terrains;
            Pictures = pictures;
        }

        // ReSharper disable once ParameterHidesMember
        public void StartGame(Action draw, Action endGame)
        {
            EndGame = endGame;
            this.draw += draw;
            previousSignalTime = DateTime.Now.Ticks;
            var timer = new Timer(UpdateInterval) { AutoReset = true };
            timer.Elapsed += UpdateState;
            timer.Start();
        }
        private void UpdateState (object sender, ElapsedEventArgs e)
        {
            var deltaT = e.SignalTime.Ticks - previousSignalTime;
            var reflectables = new List<IGeometryFigure>();
            reflectables.AddRange(Heroes.Select(hero => hero.HitBox));
            reflectables.AddRange(Terrains.Select(terrain => terrain.HitBox));
            previousSignalTime = e.SignalTime.Ticks;
            foreach (var hero in Heroes)
                hero.Update(deltaT, reflectables);
            foreach (var monster in Monsters)
                monster.Update(deltaT, reflectables);
            draw();
        }
    }
}