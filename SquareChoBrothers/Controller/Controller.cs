using System;
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

        }
        public void SecondPlayerKeyPress (object sender, KeyPressEventArgs e)
        {

        }

        public void GeneralKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (int) Keys.Escape)
                gameModel.EndGame();
        }
    }
}