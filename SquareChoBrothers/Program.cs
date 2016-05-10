using System.Windows.Forms;

namespace SquareChoBrothers
{
    internal static class Program
    {
        private static void Main()
        {
            //hui
            var game = new Game();
            Application.Run(new GameForm(game));
        }
    }
}