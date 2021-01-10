using System;
using System.Globalization;
using System.Windows.Data;

namespace AGAWeatherApp.ViewModel.ValueConverters
{
    /// <summary>
    /// The class that converting boolean to a readable text indicating the raining condition, and vice versa.
    /// </summary>
    public class BoolToRainConverter : IValueConverter
    {
        /// <summary>
        /// The method converting boolean to a readable text indicating if it's raining or not.
        /// </summary>
        /// <param name="value">The boolean value.</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>String indicates if it's raining or not</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return "Currently raining";
            return "Currently not raining";
        }

        /// <summary>
        /// The method converting back the readable text indicating if it's raining or not to boolean.
        /// </summary>
        /// <param name="value">The String indicates if it's raining or not.</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns>The boolean value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == "Currently raining";
        }
    }
}
