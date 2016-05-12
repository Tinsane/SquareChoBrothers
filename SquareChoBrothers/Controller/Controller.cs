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
            switch (keyEventArgs.KeyCode)
            {
                case Keys.Left:
                    gameModel.Heroes[0].MoveLeft();
                    break;
                case Keys.Right:
                    gameModel.Heroes[0].MoveRight();
                    break;
                case Keys.Up:
                    gameModel.Heroes[0].Jump();
                    break;
            }
        }
        public void SecondPlayerKeyDown (object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.KeyCode)
            {
                case Keys.A:
                    break;
                case Keys.D:
                    break;
                case Keys.W:
                    break;
            }
        }

        public void GeneralKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.KeyCode == Keys.Escape)
                gameModel.EndGame();
        }

        public void FirstPlayerKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.KeyCode)
            {
                case Keys.Left:
                    gameModel.Heroes[0].Stay();
                    break;
                case Keys.Right:
                    gameModel.Heroes[0].Stay();
                    break;
            }
        }

        public void SecondPlayerKeyUp(object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.KeyCode)
            {
                case Keys.A:
                    break;
                case Keys.D:
                    break;
            }
        }
    }
}