using System.Windows.Forms;
using SquareChoBrothers.Model;
using SquareChoBrothers.View;

namespace SquareChoBrothers
{
    static class Program
    {
        private static void Main()
        {
            var game = new GameModel();
            var controller = new Controller.Controller(game);
            Application.Run(new GameForm(game, controller));
        }
    }
}