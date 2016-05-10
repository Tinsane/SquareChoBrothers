using System.Windows.Forms;

namespace SquareChoBrothers
{
    static class Program
    {
        private static void Main()
        {
            var game = new Game();
            Application.Run(new GameForm(game));
        }
    }
}