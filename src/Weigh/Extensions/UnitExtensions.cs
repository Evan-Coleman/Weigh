using System;

namespace Weigh.Extensions
{
    public static class UnitExtensions
    {
        public static double KilogramsToPounds(this double weight)
        {
            return weight * 2.2046226218488;
        }

        public static double PoundsToKilograms(this double weight)
        {
            return weight * 0.45359237;
        }

        public static (int, int) CentimetersToFeetInches(this double centimeters)
        {
            var feet = centimeters / 2.54 / 12;
            var iFeet = (int)feet;
            var inches = (feet - iFeet) * 12;

            return ((int)feet, Convert.ToInt32(inches));
        }

        public static double FeetInchesToCentimeters(int feet, int inches)
        {
            return feet * .3048 + inches * 2.54 / 100;
        }
    }
}