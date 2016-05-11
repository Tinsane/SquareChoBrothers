using System;
using System.Drawing;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class GameModel
    {
        public Action EndGame;
        public Picture Background;
        private Hero[] heroes;
        private Monster[] monsters;
        private Terrain[] terrains;
        public GameModel()
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e4), Brushes.Maroon);
        }
        public void Initialize()
        {
        }
    }
}