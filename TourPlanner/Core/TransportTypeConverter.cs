using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace TourPlanner.Core
{
    public class TransportTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ComboBoxItem cbItem)
            {
               if((string)cbItem.Content == "Car")
                {
                    return "fastest";
                }

                if ((string)cbItem.Content == "Bicycle")
                {
                    return "bicycle";
                }

                if ((string)cbItem.Content == "On Foot")
                {
                    return "pedestrian";
                }
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
    }
}
