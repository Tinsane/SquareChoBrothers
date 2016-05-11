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
                    gameModel.Heroes[0].ChangeVelocity(new Geometry.Vector(-100000, 0));
                    break;
                case (int) Keys.Right:
                    gameModel.Heroes[0].ChangeVelocity(new Geometry.Vector(100000, 0));
                    break;
                case (int) Keys.Up:
                    gameModel.Heroes[0].ChangeVelocity(new Geometry.Vector(0, 100000));
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