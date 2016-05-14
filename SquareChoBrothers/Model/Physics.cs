using Geometry;

namespace SquareChoBrothers.Model
{
    public static class Physics
    {
        public static readonly Vector GravityVector;
        public const double Impulse = 100;
        public const double SpeedOfLight = 300;
        static Physics()
        {
            GravityVector = new Vector(0, 30);
        }
    }
}