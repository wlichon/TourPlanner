using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TourPlanner.Core
{
    /*
    public class TimeSpanConverterMinutes : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double minutes)
            {
                return TimeSpan.FromMinutes(minutes).Seconds(0);
            }

            return TimeSpan.Zero;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TimeSpan timeSpan)
            {
                return timeSpan.TotalMinutes;
            }

            return 0d;
        }
    }

    */
}
