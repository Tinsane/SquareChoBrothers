using System;
using System.Windows.Forms;

namespace SquareChoBrothers
{
    internal static class Program
    {
        private static void Main()
        {
            var game = new Game();
            Application.Run(new GameForm(game));
        }
    }
}