namespace Geometry
{
    public interface IStrictIntersectable<T>
    {
        bool StrictlyIntersectsWith(T figure);
    }
}