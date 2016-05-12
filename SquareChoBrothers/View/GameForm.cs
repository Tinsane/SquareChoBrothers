using System.Windows.Forms;
using SquareChoBrothers.Model;

namespace SquareChoBrothers.View
{
    public sealed class GameForm : Form
    {
        private readonly GameModel gameModel;

        public GameForm(GameModel gameModel, Controller.Controller controller)
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            this.gameModel = gameModel;
            KeyDown += controller.GeneralKeyPress;
            KeyDown += controller.FirstPlayerKeyPress;
            KeyDown += controller.SecondPlayerKeyPress;
            gameModel.EndGame = Close;
            gameModel.Draw = Invalidate;
            gameModel.Start();
        }

        private static void PaintDrawable(PaintEventArgs e, IDrawable drawable)
        {
            e.Graphics.FillRectangle(drawable.Brush, drawable.GraphicalPosition);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintDrawable(e, gameModel.Background);
            foreach (var hero in gameModel.Heroes)
                PaintDrawable(e, hero);
        }
    }
}