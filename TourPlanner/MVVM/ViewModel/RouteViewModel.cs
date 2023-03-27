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
using TourPlanner.Models.Models;
using System.Runtime.Intrinsics.Arm;
using System.IO;
using System.Windows.Media.Imaging;

namespace TourPlanner.MVVM.ViewModel
{
    public class RouteViewModel : ObservableObject
    {


        private Tour _selectedTour;
        private BitmapImage? _route;

        public BitmapImage? Route
        {
            get { return _route; }
            set { 
                _route = value;
                OnPropertyChanged();
            }
        }


        public Tour SelectedTour
        {
            get { return _selectedTour; }
            set { 
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    LoadMap();
                    OnPropertyChanged();

                }

            }
        }



        public async Task LoadMap()
        {
            var dp = new DirectionsProcessor();
            (byte[]? jpeg, string message) = await dp.LoadMap("placeholder");

            if(jpeg != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                
                    _route = new BitmapImage();
                    _route.BeginInit();
                    _route.StreamSource = new System.IO.MemoryStream(jpeg);
                    _route.EndInit();

                }

            }

            Route = _route;


            MessageBox.Show(message);



        }


        public RelayCommand Load { get; set; }

        
        public RouteViewModel()
        {
            Route = new BitmapImage();


        }

    }
}
