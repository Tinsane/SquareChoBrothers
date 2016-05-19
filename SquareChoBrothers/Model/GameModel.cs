using System;
using System.Threading;

namespace SquareChoBrothers.Model
{
    public class GameModel
    {
        private const int UpdateInterval = 3;
        public const double CellSize = 50;
        private Action draw;
        private long lastTick;

        public Map Map;
        public bool GameEnded { get; private set; }
        private Timer physicsTimer;

        public GameModel()
        {
            Map = new Map();
        }

        public GameModel(Map map)
        {
            Map = map;
        }

        public Action CloseGame { get; private set; }

        // ReSharper disable once ParameterHidesMember
        public void StartGame(Action draw, Action closeGame)
        {
            this.draw = draw;
            GameEnded = false;
            CloseGame = closeGame;
            foreach (var hero in Map.Heroes)
                hero.Alive = true;
            physicsTimer = new Timer(UpdateState, null, UpdateInterval, Timeout.Infinite);
            lastTick = DateTime.Now.Ticks;
        }

        private void CongratulateWinner()
        {
            lock (this)
            {
                Map.Clear();
                draw();
                GameEnded = true;
            }
        }

        private void UpdateState(object state)
        {
            lock (this)
            {
                var now = DateTime.Now.Ticks;
                foreach (var monster in Map.Monsters)
                    monster.Update(now - lastTick, Map);
                foreach (var hero in Map.Heroes)
                    hero.Update(now - lastTick, Map);
                foreach (var boundaryChainsaw in Map.BoundaryChainsaws)
                    boundaryChainsaw.Update(now - lastTick, Map);
                lastTick = DateTime.Now.Ticks;
                draw();
                if (!Map.Heroes[0].Alive || !Map.Heroes[1].Alive)
                {
                    CongratulateWinner();
                    return;
                }
                physicsTimer.Change(UpdateInterval, Timeout.Infinite);
            }
        }
    }
}