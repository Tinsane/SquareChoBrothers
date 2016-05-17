﻿using Geometry;

namespace SquareChoBrothers.Model
{
    public static class Physics
    {
        public const double Impulse = 3e4;
        public const double SpeedOfLight = 7e4;
        public static readonly Vector GravityVector;

        static Physics()
        {
            GravityVector = new Vector(0, 5e6);
        }
    }
}
