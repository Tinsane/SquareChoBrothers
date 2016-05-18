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
                case Keys.Escape:
                    builderModel.Close();
                    break;
                case Keys.Space:
                    builderModel.AddTerrain();
                    break;
                case Keys.H:
                    builderModel.AddHero();
                    break;
                case Keys.M:
                    builderModel.AddMonster();
                    break;
                case Keys.Delete:
                    builderModel.Delete();
                    break;
            }
        }
    }
}