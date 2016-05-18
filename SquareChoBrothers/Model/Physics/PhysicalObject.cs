using System.Drawing;
using Geometry;
using SquareChoBrothers.View;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model.Physics
{
    public abstract class PhysicalObject<T> : IDrawable
        where T : IGeometryFigure
    {
        protected PhysicalObject(Rectangle graphicalPosition, Brush brush, T hitBox, double mass)
        {
            HitBox = hitBox;
            GraphicalPosition = graphicalPosition;
            Brush = brush;
            Mass = mass;
        }

        public double Mass { get; }
        public T HitBox { get; protected set; }
        public Rectangle GraphicalPosition { get; }
        public Brush Brush { get; }
    }
}