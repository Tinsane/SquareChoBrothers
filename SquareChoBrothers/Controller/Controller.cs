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

        public void FirstPlayerKeyPress(object sender, KeyPressEventArgs e)
        {
            switch ((int) e.KeyChar)
            {
                case (int) Keys.Left:
                    break;
                case (int) Keys.Right:
                    break;
                case (int) Keys.Up:
                    break;
            }
        }
        public void SecondPlayerKeyPress (object sender, KeyPressEventArgs e)
        {
            switch ((int) e.KeyChar)
            {
                case (int) Keys.A:
                    break;
                case (int) Keys.D:
                    break;
                case (int) Keys.W:
                    break;
            }
        }

        public void GeneralKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int) Keys.Escape)
                gameModel.EndGame();
        }
    }
}