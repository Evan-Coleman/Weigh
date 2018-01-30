using System;
using System.Globalization;
using Xamarin.Forms;

namespace Weigh.Converters
{
    public class StringNullOrEmptyBoolConverter : IValueConverter
    {
        /// <summary>
        ///     Returns false if string is null or empty
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;

            return !string.IsNullOrWhiteSpace(s);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}