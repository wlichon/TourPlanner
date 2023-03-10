using TourPlanner.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
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
                    OnPropertyChanged();

                }

            }
        }
        public RouteViewModel()
        {

            
    

        }

    }
}
