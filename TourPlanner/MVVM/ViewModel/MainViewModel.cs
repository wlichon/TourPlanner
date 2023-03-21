using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Core;
using TourPlanner.Models;
using TourPlanner.Models.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {

        private string _tourBoxContent;

        private Tour _selectedTour;

        private ObservableCollection<Tour> _tours;

        private TourProcessor _tp;

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
                
                RouteVM.SelectedTour = value;
                GeneralVM.SelectedTour = value;
                _selectedTour = value;
                OnPropertyChanged();
                    
                
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

        private async Task Test(string json)
        {
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            await ApiHelper.ApiClient.PostAsync("http://localhost:7136/api/tour", data);

            //var data = new StringContent(json, Encoding.UTF8, "application/json");
            //var response = await ApiHelper.ApiClient.GetAsync("http://localhost:7136/api/tour");

            //var cont = response.Content;

        }

        private async Task AddTour()
        {
            var newTour = new Tour { TourName = _tourBoxContent};
            bool success = await _tp.AddTour(newTour);

            if (success)
            {
                var updatedTours = await _tp.LoadTours();
                Tours.Clear();
                foreach (var tour in updatedTours)
                {
                    Tours.Add(tour);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }

            TourBoxContent = "";
        }

        public async Task DeleteTour(int? tourId)
        {
            bool success = await _tp.DeleteTour(tourId);

            if (success)
            {
                var updatedTours = await _tp.LoadTours();
                Tours.Clear();
                foreach (var tour in updatedTours)
                {
                    Tours.Add(tour);
                }
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }

            TourBoxContent = "";
        }

        public MainViewModel()
        {
            _tp = new TourProcessor();

            _tours = new ObservableCollection<Tour>();

            TourBoxContent = "";

            GeneralVM = new GeneralViewModel();

            RouteVM = new RouteViewModel();

            OtherVM = new OtherViewModel();



            AddTourButton = new RelayCommand(async o => {
                await AddTour();
            });

            RemoveTourButton = new RelayCommand(async o =>
            {
                
                await DeleteTour(_selectedTour?.TourId);
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
