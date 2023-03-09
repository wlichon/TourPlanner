using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TourPlanner.Core;

namespace TourPlanner.MVVM.Model
{
    internal class Tour : ObservableObject
    {
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




        //public ObservableCollection<TourLogModel> TourLogs { get; set; }  
    }
}
