﻿using log4net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourPlanner.Core;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject, IDataErrorInfo
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public string Error { get { return null; } }

        public Dictionary<string ,string> ErrorCollection { get; private set; } =  new Dictionary<string, string>();
        public string this[string propName]
        {
            get
            {
                string result = null;

                switch (propName)
                {
                    case "TourBoxContent":
                        if (string.IsNullOrEmpty(TourBoxContent))
                        {
                            result = "Your tour name needs at least 1 character";
                        }
                        break;
                }

                if (ErrorCollection.ContainsKey(propName))
                    ErrorCollection[propName] = result;
                else if (result != null)
                    ErrorCollection.Add(propName, result);

                OnPropertyChanged(nameof(ErrorCollection));

                return result;
            }
        }
        
     

        private string _tourBoxContent;

        private Tour _selectedTour;

        private ObservableCollection<Tour> _tours;

        private ObservableCollection<Tour> _filteredTours;

        private TourProcessor _tp;

        public string TourBoxContent
        {
            get { return _tourBoxContent; }
            set {
                _tourBoxContent = value;
                Search();
                OnPropertyChanged();
            }
        }

        private bool _filterByPopularity;

        public bool FilterByPopularity
        {
            get { return _filterByPopularity; }
            set { 
                _filterByPopularity = value;
                if(_filterByPopularity == true)
                {
                    FilteredTours = new ObservableCollection<Tour>(Tours.OrderByDescending(obj => obj.TourPopularity()).ToList());
                }
                else
                {
                    FilteredTours = FilteredTours = new ObservableCollection<Tour>(_tours.Where(item => item.ChildFriendly() == true));
                }
            }
        }

        public RelayCommand SearchCommand { get; set; }

        public RelayCommand AddTourButton { get; set; }
        public RelayCommand RemoveTourButton { get; set; }

        
        public RelayCommand GeneralViewCommand { get; set; }

        public RelayCommand RouteViewCommand { get; set; }

        public RelayCommand OtherViewCommand { get; set; }

        public GeneralViewModel GeneralVM { get; set; }

        public RouteViewModel RouteVM { get; set; }

        public OtherViewModel OtherVM { get; set; }

        private TourMediator TourMediator { get; set; }

        
        public ObservableCollection<Tour> Tours {
            get { return _tours; }
        }

        public ObservableCollection<Tour> FilteredTours
        {
            get { return _filteredTours; }
            set
            {
                _filteredTours = value;
                OnPropertyChanged();
            }
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


        private async Task AddTour()
        {
            if(TourBoxContent.Length == 0)
            {
                return;
            }
            var newTour = new Tour { TourName = _tourBoxContent};
            (bool success, string addMessage) = await _tp.AddTour(newTour);

            if (success)
            {
                (var updatedTours, string loadMessage) = await _tp.LoadTours();
                if(updatedTours == null)
                {
                    MessageBox.Show(loadMessage);
                    return;
                }
                Tours.Clear();
                foreach (var tour in updatedTours)
                {
                    Tours.Add(tour);
                }

                MessageBox.Show(loadMessage);
            }
            else
            {
                MessageBox.Show(addMessage);
            }

            TourBoxContent = "";
        }

        public async Task DeleteTour(int? tourId)
        {
            (bool success, string deleteMessage) = await _tp.DeleteTour(tourId);

            if (success)
            {
                (var updatedTours, var loadMessage) = await _tp.LoadTours();
                if (updatedTours == null)
                {
                    MessageBox.Show(loadMessage);
                    return;
                }
                Tours.Clear();
                foreach (var tour in updatedTours)
                {
                    Tours.Add(tour);
                }

                MessageBox.Show(deleteMessage);
            }
            else
            {
                MessageBox.Show(deleteMessage);
            }

            TourBoxContent = "";
        }



        public void Search()
        {
            if(TourBoxContent == "")
            {
                FilteredTours = new ObservableCollection<Tour>(Tours);
                return;
            }
            else
            {
                    FilteredTours = new ObservableCollection<Tour>(_tours.Where(item => item.TourName.Contains(TourBoxContent)));
            }
        }

        public MainViewModel()
        {
            _tp = new TourProcessor();

            _tours = new ObservableCollection<Tour>();

            FilteredTours = new ObservableCollection<Tour>(Tours);

            TourBoxContent = "";

            GeneralVM = new GeneralViewModel();

            RouteVM = new RouteViewModel();

            OtherVM = new OtherViewModel();

            TourMediator = new TourMediator(RouteVM, GeneralVM);


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
