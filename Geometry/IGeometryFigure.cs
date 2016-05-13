namespace Geometry
{
    public interface IGeometryFigure : IHavingIntersectionLine<Circle>, IHavingIntersectionLine<Rectangle>
    {
        IGeometryFigure GetTransfered(Vector transferVector);
        void Transfer(Vector transferVector);
    }
}