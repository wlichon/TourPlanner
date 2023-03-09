using TourPlanner.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.MVVM.Model;
using System.Windows;

namespace TourPlanner.MVVM.ViewModel
{
    internal class RouteViewModel : ObservableObject
    {


        private Tour _selectedTour;
        private ObservableCollection<TourLog> _tourLogs = new ObservableCollection<TourLog>();

        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set { 
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    TourLogs = _selectedTour?.TourLogs ?? new ObservableCollection<TourLog>();
                    OnPropertyChanged();

                }

            }
        }
        

        public ObservableCollection<TourLog> TourLogs
        {
            get { return _tourLogs; }
            set 
            { 
                _tourLogs = value;
                OnPropertyChanged();
            }
        }
        
        public RouteViewModel()
        {
      

            TourLogs = new ObservableCollection<TourLog>();
            
    

        }

    }
}
