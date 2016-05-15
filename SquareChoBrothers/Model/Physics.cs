using Geometry;

namespace SquareChoBrothers.Model
{
    public static class Physics
    {
        public static readonly Vector GravityVector;
        public const double Impulse = 4e3;
        public const double SpeedOfLight = 2e4;
        static Physics()
        {
            GravityVector = new Vector(0, 5e4);
        }
    }
}