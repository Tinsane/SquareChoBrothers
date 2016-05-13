using System.Drawing;
using Geometry;
using SquareChoBrothers.View;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public abstract class PhysicalObject<T> : IDrawable
        where T : IGeometryFigure
    {
        public T HitBox { get; protected set; }
        public Rectangle GraphicalPosition { get; }
        public Brush Brush { get; }
        protected PhysicalObject(Rectangle graphicalPosition, Brush brush, T hitBox)
        {
            HitBox = hitBox;
            GraphicalPosition = graphicalPosition;
            Brush = brush;
        }
    }
}