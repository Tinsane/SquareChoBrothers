using Geometry;

namespace SquareChoBrothers.Model
{
    public static class Physics
    {
        public static readonly Vector GravityVector;
        public const double Impulse = 2e4;
        public const double SpeedOfLight = 5e4;
        static Physics()
        {
            GravityVector = new Vector(0, 2e6);
        }
    }
}