using System;
using System.Threading;

namespace SquareChoBrothers.Model
{
    public class GameModel
    {
        private const int UpdateInterval = 2;
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

        private void CongratulateWinner()
        {
            throw new NotImplementedException();
        }

        private void UpdateState(object state)
        {
            lock (this)
            {
                foreach (var monster in Map.Monsters)
                    monster.Update(UpdateInterval, Map);
                foreach (var hero in Map.Heroes)
                    hero.Update(UpdateInterval, Map);
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