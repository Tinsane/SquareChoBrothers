using System;
using System.Drawing;
using System.Timers;
using Point = Geometry.Point;
using Geometry;

namespace SquareChoBrothers.Model
{
    public class GameModel
    {
        private const double UpdateInterval = 10;
        public Action EndGame { get; private set; }
        private Action draw;
        public Picture Background;
        public Hero[] Heroes;
        public Monster[] Monsters;
        public Terrain[] Terrains;
        public GameModel()
        {
            Background = new Picture(new Square(new Point(0, 0), 1e4), Brushes.Maroon);
            Heroes = new Hero[1];
            Monsters = new Monster[0];
            Terrains = new Terrain[0];
            Heroes[0] = new Hero(new Square(new Point(100, 100), 100), Brushes.Green);
        }

        // ReSharper disable once ParameterHidesMember
        public void StartGame(Action draw, Action endGame)
        {
            var timer = new Timer(UpdateInterval) { AutoReset = true };
            timer.Elapsed += UpdateState;
            timer.Start();
            this.draw += draw;
            EndGame = endGame;
        }
        private void UpdateState (object sender, ElapsedEventArgs e)
        {
            foreach (var hero in Heroes)
                hero.UpdatePosition(UpdateInterval);
            foreach (var monster in Monsters)
                monster.UpdatePosition(UpdateInterval);
            draw();
        }
    }
}