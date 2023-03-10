using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TourPlanner.Models
{
    public class Tour
    {
        public static bool operator ==(Tour obj1, Tour obj2)
        {


            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            if (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null))
            {
                return false;
            }

            bool a = (obj1.TourName == obj2.TourName);
            bool b = obj1.TourLogs.SequenceEqual(obj2.TourLogs);
            bool c = (obj1.TourInfo.TransportType == obj2.TourInfo.TransportType);
            bool d = (obj1.TourInfo.Distance == obj2.TourInfo.Distance);
            bool e = (obj1.TourInfo.From == obj2.TourInfo.From);
            bool f = (obj1.TourInfo.To == obj2.TourInfo.To);
            bool g = (obj1.TourInfo.Description == obj2.TourInfo.Description);
            bool h = (obj1.TourInfo.EstimatedTime == obj2.TourInfo.EstimatedTime);
            bool i = (obj1.TourInfo.ImageData == obj2.TourInfo.ImageData);

            return a && b && c && d && e && f && g && h && i;
        }

        public static bool operator !=(Tour obj1, Tour obj2)
        {
            return !(obj1 == obj2);
        }

        private int _tourId;

        public int TourId
        {
            get { return _tourId; }
            set { _tourId = value; }
        }

        private string? _tourName;

        public string TourName
        {
            get { return _tourName; }
            set { _tourName = value; }
        }

        private TourInfo _tourInfo;

        public TourInfo TourInfo
        {
            get { return _tourInfo; }
            set { _tourInfo = value; }
        }

        private ObservableCollection<TourLog> _tourLogs = new ObservableCollection<TourLog>();

        public ObservableCollection<TourLog> TourLogs
        {
            get { return _tourLogs; }
            set { 
                _tourLogs = value;
            }
        }

    }
}
