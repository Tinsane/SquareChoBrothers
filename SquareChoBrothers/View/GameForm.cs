using System.Drawing;
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
            KeyDown += controller.GeneralKeyDown;
            KeyDown += controller.FirstPlayerKeyDown;
            KeyDown += controller.SecondPlayerKeyDown;
            gameModel.StartGame(Invalidate, Close);
        }

        private static void PaintDrawable(PaintEventArgs e, IDrawable drawable)
        {
            e.Graphics.FillRectangle(drawable.Brush, drawable.GraphicalPosition);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            lock (e)
            {
                foreach (var drawable in gameModel.map.AllDrawables)
                    PaintDrawable(e, drawable);
            }
        }
    }
}