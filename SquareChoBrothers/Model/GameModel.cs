using System;
using System.Collections.Generic;
using System.Drawing;
using System.Timers;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;
using Geometry;
using System.Linq;

namespace SquareChoBrothers.Model
{
    public class GameModel
    {
        private const double UpdateInterval = 10;
        public Action EndGame { get; private set; }
        private Action draw;
        public Picture Background;
        public Picture[] Pictures;
        public Hero[] Heroes;
        public Monster[] Monsters;
        public Terrain[] Terrains;
        private double previousSignalTime;

        public GameModel()
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4), Brushes.Maroon);
            Heroes = new Hero[1];
            Monsters = new Monster[0];
            Terrains = new Terrain[1];
            Terrains[0] = new Terrain(new Rectangle(new Point(400, 200), 200, 50), Brushes.Green);
            Pictures = new Picture[0];
            Heroes[0] = new Hero(new Rectangle(new Point(50, 50), 50, 50),
                new TextureBrush(Properties.Resources.Hero1));
        }

        public GameModel(Terrain[] terrains, Hero[] heroes,
            Monster[] monsters, Picture[] pictures)
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4, 1e4), Brushes.Maroon);
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