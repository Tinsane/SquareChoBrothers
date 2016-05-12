using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MapBuilder.Model;

namespace MapBuilder.Controller
{
    public class BuilderController
    {
        private readonly BuilderModel builderModel;

        public BuilderController(BuilderModel model)
        {
            builderModel = model;
        }

        public void GeneralKeyDown(object sender, KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.Up:
                    builderModel.TryUp();
                    break;
                case Keys.Down:
                    builderModel.TryDown();
                    break;
                case Keys.Left:
                    builderModel.TryLeft();
                    break;
                case Keys.Right:
                    builderModel.TryRight();
                    break;
            }
        }
    }
}
