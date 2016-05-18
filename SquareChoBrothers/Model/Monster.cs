using System.Drawing;
using Geometry;
using SquareChoBrothers.Model.Physics;

namespace SquareChoBrothers.Model
{
    public class Monster : DynamicPhysicalObject<Circle>
    {
        private float rotationAngle;

        public Monster(Square graphicalPosition, Brush brush, double mass)
            : base(graphicalPosition, brush,
                new Circle(graphicalPosition), mass)
        {
            rotationAngle = 0;
        }

        private void Rotate(double deltaTime)
        {
            lock (this)
            {
                rotationAngle += (float) deltaTime;
                var textureBrush = (TextureBrush) Brush;
                textureBrush.TranslateTransform((float) GraphicalPosition.Width/2,
                    (float) GraphicalPosition.Height/2);
                textureBrush.RotateTransform(rotationAngle*2);
                textureBrush.TranslateTransform((float) -GraphicalPosition.Width/2,
                    (float) -GraphicalPosition.Height/2);
            }
        }

        public void Update(double deltaTime, Map map)
        {
            Update(deltaTime, map.Terrains);
            Rotate(deltaTime);
        }
    }
}