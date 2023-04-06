using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TourPlanner.Core
{
    public class DateTimeConverterMinutes : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int minutes)
            {
                    /*
                if(parameter is Calendar calendar)
                {
                    calendar.
                    DateTime newDateTime = new DateTime(originalDateTime.Year, originalDateTime.Month, originalDateTime.Day, originalDateTime.Hour, minutes, originalDateTime.Second);

                    return newDateTime;
                }

                */

            }

            return parameter;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.Minute;
            }

            return 0;
        }
    }
}
