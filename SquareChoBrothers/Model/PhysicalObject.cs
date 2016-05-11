using System.Drawing;
using Geometry;
using SquareChoBrothers.View;
using Rectangle = Geometry.Rectangle;

namespace SquareChoBrothers.Model
{
    public abstract class PhysicalObject<T> : IDrawable 
        where T : IIntersectable<Circle>, IIntersectable<Rectangle>, IHavingIntersectionLine<T>
    {
        public T HitBox { get; set; }
        public Rectangle GraphicalPosition { get; }
        public Brush Brush { get; }
        protected PhysicalObject(Rectangle graphicalPosition, Brush brush)
        {
            GraphicalPosition = graphicalPosition;
            Brush = brush;
        }
    }
}