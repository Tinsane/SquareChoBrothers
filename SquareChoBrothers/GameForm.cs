using System.Windows.Forms;

namespace SquareChoBrothers
{
    public class GameForm : Form
    {
        private Game game;
        protected override void OnPaint (PaintEventArgs e)
        {
        }
        public GameForm(Game game)
        {
            this.game = game;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            game.Start();
        }
    }
}