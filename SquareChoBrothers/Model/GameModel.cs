using System;
using System.Threading;
using System.Threading.Tasks;

namespace SquareChoBrothers.Model
{
    public class GameModel
    {
        private const int UpdateInterval = 3;
        public const double CellSize = 50;
        private Action draw;

        public Map Map;
        private Timer physicsTimer;
        private long lastTick;

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
            foreach (var hero in Map.Heroes)
                hero.Alive = true;
            physicsTimer = new Timer(UpdateState, null, UpdateInterval, Timeout.Infinite);
            lastTick = DateTime.Now.Ticks;
        }

        private void CongratulateWinner()
        {
            throw new NotImplementedException();
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