using System.Drawing;
using System.Windows.Forms;
using SquareChoBrothers.Model;
using SquareChoBrothers.View;
using Newtonsoft.Json;
using System.IO;

namespace SquareChoBrothers
{
    static class Program
    {
        private static void Main()
        {
            var fileName = "map.txt";
            var map = JsonConvert.DeserializeObject<Map>(File.ReadAllText(fileName));
            var game = new GameModel(map);
            var controller = new Controller.Controller(game);
            Application.Run(new GameForm(game, controller));
        }
    }
}