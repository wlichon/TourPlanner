using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class TourLog : IEquatable<TourLog>
    {
        public bool Equals(TourLog other)
        {
            if (other is null)
                return false;

            return this.Distance == other.Distance && this.Duration == other.Duration && this.Date == other.Date;
        }

        public override bool Equals(object obj) => Equals(obj as TourLog);

        public override int GetHashCode() => (_distance).GetHashCode();
        public static bool operator ==(TourLog obj1, TourLog obj2)
        {


            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            if (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null))
            {
                return false;
            }



            return (obj1.Distance == obj2.Distance &&
                    obj1.Duration == obj2.Duration &&
                    obj1.Date == obj2.Date);
        }

        public static bool operator !=(TourLog obj1, TourLog obj2)
        {
            return !(obj1 == obj2);
        }

        public int TourLogId { get; set; }

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

        public int TourId { get; set; }




    }
}
