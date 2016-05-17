namespace Geometry
{
    public class Geom
    {
        public static double Precision = 1e-7;

        public static double GetPrecision()
        {
            return Precision;
        }

        public static void SetPrecision(double precision)
        {
            Precision = precision;
        }
    }
}