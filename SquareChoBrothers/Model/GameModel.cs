using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using Geometry;
using SquareChoBrothers.Model.Factories;
using SquareChoBrothers.Properties;
using Point = Geometry.Point;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public class GameModel
    {
        private const int UpdateInterval = 2;
        public const double CellSize = 50;

        public Map map;

        public GameModel()
        {
            map = new Map();
        }

        public GameModel(Map map)
        {
            this.map = map;
        }

        public Action EndGame { get; private set; }

        // ReSharper disable once ParameterHidesMember
        public void StartGame(Action draw, Action endGame)
        {
            this.draw = draw;
            EndGame = endGame;
            physicsTimer = new Timer(UpdateState, null, UpdateInterval, Timeout.Infinite);
        }

        private void UpdateState(object state)
        {
            lock (this)
            {
                var reflectables = new List<IGeometryFigure>();
                reflectables.AddRange(map.Heroes.Select(hero => hero.HitBox));
                reflectables.AddRange(map.Terrains.Select(terrain => terrain.HitBox));
                foreach (var hero in map.Heroes)
                    hero.Update(UpdateInterval, reflectables);
                foreach (var monster in map.Monsters)
                    monster.Update(UpdateInterval, reflectables);
                physicsTimer.Change(UpdateInterval, Timeout.Infinite);
                draw();
            }
        }
    }
}
