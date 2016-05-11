namespace Geometry
{
    public interface IIntersectable<T>
    {
        bool IntersectsWith(T figure);
    }
}