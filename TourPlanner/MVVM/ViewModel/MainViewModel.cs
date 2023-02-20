using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernDesign.Core;

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
