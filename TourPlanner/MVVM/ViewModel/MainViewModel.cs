using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModernDesign.Core;
using TourPlanner.MVVM.Model;

namespace TourPlanner.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        private string _tourBoxContent;

        private Tour _selectedTour;

        private ObservableCollection<Tour> _tours;

        public string TourBoxContent
        {
            get { return _tourBoxContent; }
            set { 
                _tourBoxContent = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddTourButton { get; set; }
        public RelayCommand RemoveTourButton { get; set; }
        public RelayCommand GeneralViewCommand { get; set; }

        public RelayCommand RouteViewCommand { get; set; }

        public RelayCommand OtherViewCommand { get; set; }

        public GeneralViewModel GeneralVM { get; set; }

        public RouteViewModel RouteVM { get; set; }

        public OtherViewModel OtherVM { get; set; }

        public ObservableCollection<Tour> Tours {
            get { return _tours; }
            
        }


        public Tour SelectedTour { 
            get { return _selectedTour; }
            set
            {
                if(value != _selectedTour)
                {
                    _selectedTour = value;
                    OnPropertyChanged();
                }
            }
        }



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

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //System.Windows.MessageBox.Show("Firing");
        }

        public MainViewModel()
        {
            _tours = new ObservableCollection<Tour>();
            _tours.CollectionChanged += OnCollectionChanged;

            TourBoxContent = "";
            

            for(int i= 0; i < 5; i++)
            {
                Tours.Add(new Tour
                {
                    TourId = 0,
                    Tourname = "Andreaspark"
                });

            }

            for (int i = 0; i < 5; i++)
            {
                Tours.Add(new Tour
                {
                    TourId = 1,
                    Tourname = "Schoenbrunn"
                });

            }

            for (int i = 0; i < 5; i++)
            {
                Tours.Add(new Tour
                {
                    TourId = 2,
                    Tourname = "Kahlenberg"
                });

            }


            GeneralVM = new GeneralViewModel();

            RouteVM = new RouteViewModel();

            OtherVM = new OtherViewModel();

            AddTourButton = new RelayCommand(o =>
            {
                Tours.Add(new Tour { TourId = 0, Tourname = TourBoxContent });

                TourBoxContent = "";
            });

            RemoveTourButton = new RelayCommand(o =>
            {
                var Tour = Tours.FirstOrDefault(x => x.Tourname == TourBoxContent);
                if (Tour != null)
                {
                    Tours.Remove(Tour);
                }
                TourBoxContent = "";
            });

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
