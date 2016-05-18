using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Geometry;

namespace SquareChoBrothers.Model
{
    public class GameModel
    {
        private const int UpdateInterval = 10;
        public const double CellSize = 50;
        private Action draw;

        public Map Map;
        private Timer physicsTimer;

        public GameModel()
        {
            Map = new Map();
        }

        public GameModel(Map map)
        {
            Map = map;
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
                reflectables.AddRange(Map.Heroes.Select(hero => hero.HitBox));
                reflectables.AddRange(Map.Terrains.Select(terrain => terrain.HitBox));
                foreach (var hero in Map.Heroes)
                    hero.Update(UpdateInterval, reflectables);
                foreach (var monster in Map.Monsters)
                    monster.Update(UpdateInterval, reflectables);
                physicsTimer.Change(UpdateInterval, Timeout.Infinite);
                draw();
            }
        }
    }
}