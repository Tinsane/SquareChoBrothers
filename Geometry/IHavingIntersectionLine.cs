namespace Geometry
{
    public interface IHavingIntersectionLine<T> : IIntersectable<T>
    {
        Line GetIntersectionLine(T figure);
    }
}