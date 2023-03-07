using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TourPlanner.MVVM.Model
{
    internal class Tour
    {
        private int _tourId;

        public int TourId
        {
            get { return _tourId; }
            set { _tourId = value; }
        }

        private string? _tourname;

        public string Tourname
        {
            get { return _tourname; }
            set { _tourname = value; }
        }


        //public ObservableCollection<TourLogModel> TourLogs { get; set; }  
    }
}
