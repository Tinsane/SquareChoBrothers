namespace Geometry
{
    public interface IHavingIntersectionLine<T> : IStrictIntersectable<T>
    {
        Line GetIntersectionLine(T figure);
    }
}