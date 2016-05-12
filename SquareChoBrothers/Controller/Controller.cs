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

        public void FirstPlayerKeyPress(object sender, KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.KeyCode)
            {
                case Keys.Left:
                    gameModel.Heroes[0].ChangeVelocity(new Geometry.Vector(-10, 0));
                    break;
                case Keys.Right:
                    gameModel.Heroes[0].ChangeVelocity(new Geometry.Vector(10, 0));
                    break;
                case Keys.Up:
                    gameModel.Heroes[0].ChangeVelocity(new Geometry.Vector(0, -10));
                    break;
            }
        }
        public void SecondPlayerKeyPress (object sender, KeyEventArgs keyEventArgs)
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

        public void GeneralKeyPress(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.KeyCode == Keys.Escape)
                gameModel.EndGame();
        }
    }
}