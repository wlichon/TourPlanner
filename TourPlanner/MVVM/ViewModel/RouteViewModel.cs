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
    public class RouteViewModel : BaseComponent
    {


        private Tour _selectedTour;
        private BitmapImage? _routeImage;

        public BitmapImage? RouteImage
        {
            get { return _routeImage; }
            set { 
                _routeImage = value;
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

            if (SelectedTour.TourInfo.ImageData == null && SelectedTour.TourInfo.From != null && SelectedTour.TourInfo.To != null) // enter when Imagedata is null but From and To are set
                (SelectedTour.TourInfo.ImageData, message) = await dp.LoadMap(SelectedTour.TourInfo.From, SelectedTour.TourInfo.To);

            if(SelectedTour.TourInfo.ImageData == null) // enter when either From or To or both are null
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


        public RelayCommand Load { get; set; }

        
        public RouteViewModel()
        {
            RouteImage = new BitmapImage();


        }

    }
}
