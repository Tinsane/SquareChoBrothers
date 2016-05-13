using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapBuilder.Controller;
using MapBuilder.Model;
using MapBuilder.View;

namespace MapBuilder
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var model = new BuilderModel();
            Application.Run(new BuilderForm(model, new BuilderController(model)));
        }
    }
}
