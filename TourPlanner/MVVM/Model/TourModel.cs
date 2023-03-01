using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TourPlanner.MVVM.Model
{
    internal class TourModel
    {
        private int _tourID;

        public int TourID
        {
            get { return _tourID; }
            set { _tourID = value; }
        }

        private string _tourname;

        public string Tourname
        {
            get { return _tourname; }
            set { _tourname = value; }
        }


        //public ObservableCollection<TourLogModel> TourLogs { get; set; }  
    }
}
