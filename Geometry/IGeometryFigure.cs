namespace Geometry
{
    public interface IGeometryFigure : IHavingIntersectionLine<Circle>, IHavingIntersectionLine<Rectangle>
    {
        void Transfer(Vector transferVector);
    }
}