using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.MVVM.Model
{
    internal class TourLogModel
    {
        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        private TimeSpan _duration;

        public TimeSpan Duration
        {
            get { return _duration; }
            set { _duration = value; }
        }

        private int _distance;

        public int Distance
        {
            get { return _distance; }
            set { _distance = value; }
        }




    }
}
