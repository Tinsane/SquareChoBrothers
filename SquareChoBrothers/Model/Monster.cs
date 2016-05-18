using System.Collections.Generic;
using System.Drawing;
using Geometry;
using SquareChoBrothers.Model.Physics;

namespace SquareChoBrothers.Model
{
    public class Monster : DynamicPhysicalObject<Circle>
    {
        private float rotationAngle;
        public Monster(Square graphicalPosition, Brush brush)
            : base(new Square(graphicalPosition.Center, graphicalPosition.Size), brush, new Circle(graphicalPosition.Center, graphicalPosition.Size/2))
        {
            rotationAngle = 0;
        }

        public new void Update(double deltaTime, List<IGeometryFigure> reflectables)
        {
            base.Update(deltaTime, reflectables);
            lock (this)
            {
                rotationAngle += (float) deltaTime;
                ((TextureBrush) Brush).TranslateTransform((float) GraphicalPosition.Width/2,
                    (float) GraphicalPosition.Height/2);
                ((TextureBrush) Brush).RotateTransform(rotationAngle*2);
                ((TextureBrush) Brush).TranslateTransform((float) -GraphicalPosition.Width/2,
                    (float) -GraphicalPosition.Height/2);
            }
        }
    }
}