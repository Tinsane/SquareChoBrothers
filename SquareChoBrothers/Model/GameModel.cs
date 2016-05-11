using System;
using System.Drawing;
using System.Timers;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class GameModel
    {
        private const double UpdateInterval = 1;
        public Action EndGame;
        public Action Draw;
        public Picture Background;
        public Hero[] Heroes;
        public Monster[] Monsters;
        public Terrain[] Terrains;

        public GameModel()
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4), Brushes.Maroon);
            Heroes = new Hero[1];
            Heroes[0] = new Hero(new Rectangle(new Point(0, 0), 100), Brushes.Green);
            var timer = new Timer(UpdateInterval);
            timer.Elapsed += UpdateState;
        }
        private void UpdateState (object sender, ElapsedEventArgs e)
        {
            foreach (var hero in Heroes)
                hero.UpdatePosition(UpdateInterval);
            foreach (var monster in Monsters)
                monster.UpdatePosition(UpdateInterval);
            Draw();
        }
    }
}