namespace Geometry
{
    public interface IGeometryFigure : IHavingIntersectionLine<Circle>, IHavingIntersectionLine<Rectangle>
    {
        Point Center { get; }
        IGeometryFigure GetTransfered(Vector transferVector);
        void Transfer(Vector transferVector);
    }
}