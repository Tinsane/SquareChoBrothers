using System.Drawing;
using Geometry;
using SquareChoBrothers.Model.Physics;

namespace SquareChoBrothers.Model
{
    public class Monster : DynamicPhysicalObject<Circle>
    {
        private float rotationAngle;

        public Monster(Square graphicalPosition, string imageName, double mass)
            : base(graphicalPosition.GetCopy(), imageName,
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

        public new void Update(double deltaTime, Map map)
        {
            base.Update(deltaTime, map);
            Rotate(deltaTime);
        }

        protected override void ResolveCollisions(double dTime, Map map)
        {
            Reflect(dTime, map.Terrains);
            Reflect(dTime, map.Monsters);
        }
    }
}