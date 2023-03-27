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



        public async Task LoadMap()
        {
            var dp = new DirectionsProcessor();
            (byte[]? jpeg, string message) = await dp.LoadMap("placeholder");

            MessageBox.Show(message);

            Map = dp.ConvertArrayToBitmap(jpeg);

            System.Windows.Media.Imaging.BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                Map.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

            System.Windows.Media.Imaging.BitmapImage bitmapImage = new System.Windows.Media.Imaging.BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = new MemoryStream();
            System.Windows.Media.Imaging.BitmapEncoder encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bitmapSource));
            encoder.Save(bitmapImage.StreamSource);
            bitmapImage.EndInit();

        }

        private BitmapImage _bitmapImage;

        public  BitmapImage bitmapImage
        {
            get { return _bitmapImage; }
            set { _bitmapImage = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand Load { get; set; }

        private Bitmap _map;
        public Bitmap Map { 
            get { return _map; } 
            set
            {
                _map = value;
                OnPropertyChanged();
            }
        }
        public RouteViewModel()
        {

            Load = new RelayCommand(async o => {
                await LoadMap();
            });


        }

    }
}
