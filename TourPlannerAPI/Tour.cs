namespace TourPlannerAPI

{
    public class Tour
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


        //public ObservableCollection<TourLogModel> TourLogs { get; set; }  
    }
}
