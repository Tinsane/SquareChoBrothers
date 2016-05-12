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
        public Action draw, close;
        public double Width, Height;
        public Terrain[] Terrains;
        public Picture[] Pictures;
        public Picture Background;
        public Point currentLocation;

        public BuilderModel()
        {
            Background = new Picture(new Rectangle(new Point(0, 0), 1e3), System.Drawing.Brushes.White);
            currentLocation = new Point(squareSize / 2, squareSize / 2);
            Width = 1e3;
            Height = 1e3;
        }

        public void StartGame(Action draw, Action close)
        {
            this.draw += draw;
            this.close += close;
        }

        private bool RightCoordinates(double x, double y) => 
            x >= 0 && x <= Width && y >= 0 && y <= Height;

        private bool RightCoordinates(Point location) => 
            RightCoordinates(location.x, location.y);

        private bool TryNewLocation(Point newLocation)
        {
            if (RightCoordinates(newLocation))
            {
                currentLocation = newLocation;
                draw();
                return true;
            }
            return false;
        }

        public bool TryUp() => TryNewLocation(currentLocation + Up);

        public bool TryDown() => TryNewLocation(currentLocation + Down);

        public bool TryLeft() => TryNewLocation(currentLocation + Left);

        public bool TryRight() => TryNewLocation(currentLocation + Right);
    }
}
