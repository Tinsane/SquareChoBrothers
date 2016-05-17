using System.Windows.Forms;
using MapBuilder.Controller;
using MapBuilder.Model;
using SquareChoBrothers.Model;
using SquareChoBrothers.View;

namespace MapBuilder.View
{
    public sealed partial class BuilderForm : Form
    {
        private readonly BuilderModel builderModel;

        public BuilderForm(BuilderModel builderModel, BuilderController controller)
        {
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.builderModel = builderModel;
            KeyDown += controller.GeneralKeyDown;
            builderModel.StartGame(Invalidate, Close);
        }

        private static void PaintDrawable(PaintEventArgs e, IDrawable drawable)
        {
            e.Graphics.FillRectangle(drawable.Brush, drawable.GraphicalPosition);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            foreach (var drawable in builderModel.Map.Drawables)
                PaintDrawable(e, drawable);
            PaintDrawable(e, builderModel.CurrentPicture);
        }
    }
}