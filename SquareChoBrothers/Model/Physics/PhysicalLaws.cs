using Geometry;

namespace SquareChoBrothers.Model.Physics
{
    public static class PhysicalLaws
    {
        public const double SpeedOfLight = 15e4;
        public const double ImpactConstant = 0.5;
        public static readonly Vector GravityVector;

        static PhysicalLaws()
        {
            GravityVector = new Vector(0, 2e7);
        }
    }
}