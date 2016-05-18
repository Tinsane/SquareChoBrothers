using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using SquareChoBrothers.Model;
using SquareChoBrothers.View;

namespace SquareChoBrothers
{
    internal static class Program
    {
        private static void Main()
        {
            const string fileName = "map.txt";
            var map = JsonConvert.DeserializeObject<Map>(File.ReadAllText(fileName));
            var game = new GameModel(map);
            var controller = new Controller.Controller(game);
            Application.Run(new GameForm(game, controller));
        }
    }
}