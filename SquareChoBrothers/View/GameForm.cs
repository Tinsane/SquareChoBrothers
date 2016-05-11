using System.Windows.Forms;
using SquareChoBrothers.Model;

namespace SquareChoBrothers.View
{
    public class GameForm : Form
    {
        private readonly GameModel gameModel;

        public GameForm(GameModel gameModel, Controller.Controller controller)
        {
            this.gameModel = gameModel;
            KeyPress += controller.GeneralKeyPress;
            KeyPress += controller.FirstPlayerKeyPress;
            KeyPress += controller.SecondPlayerKeyPress;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            gameModel.EndGame = Close;
        }

        private static void PaintDrawable(PaintEventArgs e, IDrawable drawable)
        {
            e.Graphics.FillRectangle(drawable.Brush, drawable.GraphicalPosition);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintDrawable(e, gameModel.Background);
            //foreach (var hero in gameModel.Heroes)
                //PaintDrawable(e, hero);
        }
    }
}