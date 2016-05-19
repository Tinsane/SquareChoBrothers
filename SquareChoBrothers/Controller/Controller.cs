using System.Windows.Forms;
using SquareChoBrothers.Model;

namespace SquareChoBrothers.Controller
{
    public class Controller
    {
        private readonly GameModel gameModel;
        public Controller(GameModel gameModel)
        {
            this.gameModel = gameModel;
        }

        public void FirstPlayerKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            lock (gameModel)
            {
                if (gameModel.GameEnded)
                    return;
                switch (keyEventArgs.KeyCode)
                {
                    case Keys.Left:
                        gameModel.Map.Heroes[0].MoveLeft();
                        break;
                    case Keys.Right:
                        gameModel.Map.Heroes[0].MoveRight();
                        break;
                    case Keys.Down:
                        gameModel.Map.Heroes[0].Stay();
                        break;
                    case Keys.Up:
                        gameModel.Map.Heroes[0].Jump(gameModel.Map);
                        break;
                }
            }
        }
        public void SecondPlayerKeyDown (object sender, KeyEventArgs keyEventArgs)
        {
            lock (gameModel)
            {
                if (gameModel.GameEnded)
                    return;
                switch (keyEventArgs.KeyCode)
                {
                    case Keys.A:
                        gameModel.Map.Heroes[1].MoveLeft();
                        break;
                    case Keys.D:
                        gameModel.Map.Heroes[1].MoveRight();
                        break;
                    case Keys.W:
                        gameModel.Map.Heroes[1].Jump(gameModel.Map);
                        break;
                    case Keys.S:
                        gameModel.Map.Heroes[1].Stay();
                        break;
                }
            }
        }

        public void GeneralKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            lock (gameModel)
            {
                if (keyEventArgs.KeyCode == Keys.Escape)
                    gameModel.CloseGame();
            }
        }
    }
}