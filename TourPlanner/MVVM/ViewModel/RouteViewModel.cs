using ModernDesign.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.MVVM.Model;

namespace TourPlanner.MVVM.ViewModel
{
    internal class RouteViewModel : ObservableObject
    {

        public ObservableCollection<TourLogModel> TourLogs { get; set; }

        private TourModel _selectedTour;

        public TourModel SelectedTour
        {
            get { return _selectedTour; }
            set { 
                _selectedTour = value;
                OnPropertyChanged();

            }
        }

        private MainViewModel _mainViewModel;


        public RouteViewModel()
        {
            //_mainViewModel = new MainViewModel();
            //_mainViewModel.PropertyChanged += MainViewModel_PropertyChanged;

            TourLogs = new ObservableCollection<TourLogModel>();

            for (int i = 0; i < 5; i++)
            {
                TourLogs?.Add(new TourLogModel
                {
                    TourID = 0,
                    Date = DateTime.Now,
                    Distance = 1500,
                    Duration = TimeSpan.FromSeconds(900)
                });

            }

            for (int i = 0; i < 5; i++)
            {
                TourLogs?.Add(new TourLogModel
                {
                    TourID = 1,
                    Date = DateTime.Now,
                    Distance = 2500,
                    Duration = TimeSpan.FromSeconds(600)
                });

            }
        }

        private void MainViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainViewModel.SelectedTour))
            {
                // Update the value of MyField in the other class
                SelectedTour = _mainViewModel.SelectedTour;
            }
        }

    }
}
