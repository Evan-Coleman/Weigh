using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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
            double Feet = (centimeters / 2.54) / 12;
            int iFeet = (int)Feet;
            double Inches = (Feet - (double)iFeet) * 12;

            return ((int)Feet,Convert.ToInt32(Inches));
        }

        public static double FeetInchesToCentimeters(int feet, int inches)
        {
            return (double)feet * .3048 + (((double)inches * 2.54) / 100);
        }
    }
}