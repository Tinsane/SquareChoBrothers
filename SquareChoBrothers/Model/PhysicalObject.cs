using System.Drawing;
using Geometry;
using SquareChoBrothers.View;

namespace SquareChoBrothers.Model
{
    public abstract class PhysicalObject<T> : IDrawable 
        where T : IGeometryFigure
    {
        protected T HitBox { get; set; }
        public Square GraphicalPosition { get; }
        public Brush Brush { get; }
        protected PhysicalObject(Square graphicalPosition, Brush brush, T hitBox)
        {
            HitBox = hitBox;
            GraphicalPosition = graphicalPosition;
            Brush = brush;
        }
    }
}