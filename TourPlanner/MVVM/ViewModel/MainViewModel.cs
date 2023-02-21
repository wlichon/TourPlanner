using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernDesign.Core;
using TourPlanner.MVVM.Model;

namespace TourPlanner.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public RelayCommand GeneralViewCommand { get; set; }

        public RelayCommand RouteViewCommand { get; set; }

        public RelayCommand OtherViewCommand { get; set; }

        public GeneralViewModel GeneralVM { get; set; }

        public RouteViewModel RouteVM { get; set; }

        public OtherViewModel OtherVM { get; set; }

        public ObservableCollection<TourModel> Tours { get; set; }

        public ObservableCollection<TourLogModel> TourLogs { get; set; }  

        public TourModel SelectedTour { get; set; }



        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Tours = new ObservableCollection<TourModel>();
            TourLogs = new ObservableCollection<TourLogModel>();

            for(int i= 0; i < 5; i++)
            {
                Tours.Add(new TourModel
                {
                    Tourname = "Exampletour"
                });

            }

            for (int i = 0; i < 5; i++)
            {
                TourLogs.Add(new TourLogModel
                {
                    Date = DateTime.Now,
                    Distance = 2500,
                    Duration = TimeSpan.FromSeconds(600)
                });

            }


            GeneralVM = new GeneralViewModel();

            RouteVM = new RouteViewModel();

            OtherVM = new OtherViewModel();

            GeneralViewCommand = new RelayCommand(o =>
            {
                CurrentView = GeneralVM;
            });

            RouteViewCommand = new RelayCommand(o =>
            {
                CurrentView = RouteVM;
            });

            OtherViewCommand = new RelayCommand(o =>
            {
                CurrentView = OtherVM;
            });

            _currentView = GeneralVM;
        }
    }
}
