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
using System.Drawing;
using System.Runtime.Intrinsics.Arm;
using System.IO;
using System.Windows.Media.Imaging;
using TourPlanner.MVVM.View;

namespace TourPlanner.MVVM.ViewModel
{
    public class RouteViewModel : BaseComponent
    {
        private Tour _selectedTour;
        private BitmapImage? _routeImage;

        private TourLog _selectedLog;

        public TourLog SelectedLog
        {
            get { return _selectedLog; }
            set
            {
                _selectedLog = value;
                OnPropertyChanged();
            }
        }


        public BitmapImage? RouteImage
        {
            get { return _routeImage; }
            set
            {
                _routeImage = value;
                OnPropertyChanged();
            }
        }


        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set
            {
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    FilteredLogs = SelectedTour?.TourLogs;
                    LoadMap();
                    OnPropertyChanged();
                }

            }
        }

        public void MediatorRefreshMap()
        {


            using (MemoryStream memory = new MemoryStream())
            {

                _routeImage = new BitmapImage();
                _routeImage.BeginInit();
                _routeImage.StreamSource = new System.IO.MemoryStream(SelectedTour.TourInfo.ImageData);
                _routeImage.EndInit();
                RouteImage = _routeImage;

            }

        }

        public async Task LoadMap()
        {
            var dp = new DirectionsProcessor();
            string message = "Map loaded from memory";

            /*
            if (SelectedTour.TourInfo.ImageData == null && SelectedTour.TourInfo.From != null && SelectedTour.TourInfo.To != null) // enter when Imagedata is null but From and To are set
                (SelectedTour.TourInfo.ImageData, message) = await dp.LoadMap(SelectedTour.TourInfo.From, SelectedTour.TourInfo.To);
            */

            if (SelectedTour.TourInfo.ImageData == null) // enter when either From or To or both are null
            {
                RouteImage = null;
                MessageBox.Show("Cannot load route image since insufficient data");
                return;
            }

            

            using (MemoryStream memory = new MemoryStream())
            {

                _routeImage = new BitmapImage();
                _routeImage.BeginInit();
                _routeImage.StreamSource = new System.IO.MemoryStream(SelectedTour.TourInfo.ImageData);
                _routeImage.EndInit();
                RouteImage = _routeImage;

            }





            MessageBox.Show(message);



        }

        public RelayCommand ShowAddLogWindowButton { get; set; }

        public RelayCommand RemoveSelectedLogButton { get; set; }

        public RelayCommand EditSelectedLogButton { get; set; }
        public RelayCommand Load { get; set; }

        private ObservableCollection<TourLog> _filteredLogs;

        private string _tourLogBoxContent;

        public string TourLogBoxContent
        {
            get { return _tourLogBoxContent; }
            set { 
                _tourLogBoxContent = value;
                Search();
            }
        }

        public ObservableCollection<TourLog> FilteredLogs { 
            get
            {
                return _filteredLogs;
            }
            set
            {
                _filteredLogs = value;
                OnPropertyChanged();
            } 
        }

        public void Search()
        {

            if (TourLogBoxContent == "")
            {
                FilteredLogs = new ObservableCollection<TourLog>(SelectedTour.TourLogs);
                return;
            }
            else
            {

                FilteredLogs = new ObservableCollection<TourLog>(SelectedTour.TourLogs.Where(item => item.Comment.Contains(TourLogBoxContent)));
            }
        }

        public RouteViewModel()
        {
            
            
            RouteImage = new BitmapImage();



            ShowAddLogWindowButton = new RelayCommand(async o =>
            {

                TourLogWindowViewModel tourLogVM = new TourLogWindowViewModel(null);

                TourLogWindowView tourLogWindow = new TourLogWindowView();

                tourLogWindow.DataContext = tourLogVM;

                bool? dialogResult = tourLogWindow.ShowDialog();

                switch (dialogResult)
                {
                    case true:
                        if (_selectedTour == null)
                            return;

                        if (_selectedTour.TourLogs == null)
                        {
                            _selectedTour.TourLogs = new ObservableCollection<TourLog>();

                        }

                        _selectedTour.TourLogs.Add(tourLogVM.TourLog);
                        var tp = new TourProcessor();
                        (bool success, string message) = await tp.UpdateTour(_selectedTour);
                        if (success)
                        {
                            await tp.LoadTours();
                            MessageBox.Show("Tour log added");
                        }
                        else
                        {
                            MessageBox.Show("Tour log adding failed");

                        }

                        //add log to tour

                        break;
                    case false:
                        //dont add
                        MessageBox.Show("tour log window closed");
                        break;
                    default:
                        //shouldnt happen
                        break;
                }

            });

            RemoveSelectedLogButton = new RelayCommand(async o =>
            {
                if (SelectedTour == null || SelectedLog == null)
                    return;

                var tp = new TourProcessor();

                (bool success, string message) = await tp.DeleteTourLog(SelectedLog.TourLogId);

                if (success)
                {
                    SelectedTour.TourLogs.Remove(SelectedLog);
                    MessageBox.Show(message);
                }
                else
                {
                    MessageBox.Show(message);

                }
            });


            EditSelectedLogButton = new RelayCommand(async o =>
            {
              
                if(SelectedLog == null)
                {
                    MessageBox.Show("You have to selected a log before you can edit it");
                    return;
                }

                TourLog? log = (TourLog)SelectedLog.Clone();
                TourLogWindowViewModel tourLogVM = new TourLogWindowViewModel(log);

                TourLogWindowView tourLogWindow = new TourLogWindowView();

                tourLogWindow.DataContext = tourLogVM;

                bool? dialogResult = tourLogWindow.ShowDialog();

                switch (dialogResult)
                {
                    case true:
                        if (_selectedTour == null)
                            return;

                        if (_selectedTour.TourLogs == null)
                        {
                            return;

                        }
                        
                        
                        SelectedLog.Date = tourLogVM.TourLog.Date;
                        SelectedLog.Duration = tourLogVM.TourLog.Duration;
                        SelectedLog.Difficulty = tourLogVM.TourLog.Difficulty;
                        SelectedLog.Rating = tourLogVM.TourLog.Rating;
                        SelectedLog.Comment = tourLogVM.TourLog.Comment;
                        


                var tp = new TourProcessor();
                        (bool success, string message) = await tp.UpdateTour(_selectedTour);
                        if (success)
                        {
                            MessageBox.Show("Tour log edited");
                        }
                        else
                        {
                            MessageBox.Show("Tour log editing failed");

                        }

                        //add log to tour

                        break;
                    case false:
                        //dont add
                        MessageBox.Show("tour log window closed");
                        break;
                    default:
                        //shouldnt happen
                        break;
                }
            });
    
            
        }
    }
}
