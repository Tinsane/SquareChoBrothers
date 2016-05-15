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
                switch (keyEventArgs.KeyCode)
                {
                    case Keys.Left:
                        gameModel.map.Heroes[0].MoveLeft();
                        break;
                    case Keys.Right:
                        gameModel.map.Heroes[0].MoveRight();
                        break;
                    case Keys.Down:
                        gameModel.map.Heroes[0].Stay();
                        break;
                    case Keys.Up:
                        gameModel.map.Heroes[0].Jump();
                        break;
                }
            }
        }
        public void SecondPlayerKeyDown (object sender, KeyEventArgs keyEventArgs)
        {
            lock (gameModel)
            {
                switch (keyEventArgs.KeyCode)
                {
                    case Keys.A:
                        gameModel.map.Heroes[1].MoveLeft();
                        break;
                    case Keys.D:
                        gameModel.map.Heroes[1].MoveRight();
                        break;
                    case Keys.W:
                        gameModel.map.Heroes[1].Jump();
                        break;
                    case Keys.S:
                        gameModel.map.Heroes[1].Stay();
                        break;
                }
            }
        }

        public void GeneralKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            lock (gameModel)
            {
                if (keyEventArgs.KeyCode == Keys.Escape)
                    gameModel.EndGame();
            }
        }
    }
}