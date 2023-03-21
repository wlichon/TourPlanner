using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models;
using TourPlanner.Models.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using TourPlanner.MVVM.ViewModel;

namespace TourPlanner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();


            //Task.Run(() => GetTours());
        }

        private async void WhenLoaded(object sender, EventArgs e)
        {
            var mainViewModel = (MainViewModel)Application.Current.MainWindow.DataContext;

            var tp = new TourProcessor();

            ObservableCollection<Tour> tours = await tp.LoadTours();

            
            mainViewModel.Tours.Clear();
            foreach (var tour in tours)
            {
                mainViewModel.Tours.Add(tour);
            }
            
        }

        private async void GetTours()
        {
            var tp = new TourProcessor();

            tp.LoadTours();
            
            //var schoenbrunnLogs = new ObservableCollection<TourLog>();
            //var andreasLogs = new ObservableCollection<TourLog>();
            //var Tours = new ObservableCollection<Tour>();

            //for (int i = 0; i < 5; i++)
            //{
            //    andreasLogs.Add(new TourLog
            //    {
            //        Date = DateTime.Now,
            //        Distance = 1500,
            //        Duration = TimeSpan.FromSeconds(900)
            //    });

            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    schoenbrunnLogs.Add(new TourLog
            //    {
            //        Date = DateTime.Now,
            //        Distance = 2500,
            //        Duration = TimeSpan.FromSeconds(600)
            //    });

            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    Tours.Add(new Tour
            //    {
            //        TourId = 0,
            //        TourName = "Andreaspark",
            //        TourInfo = new TourInfo { TransportType = "Car" },
            //        TourLogs = andreasLogs
            //    });

            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    Tours.Add(new Tour
            //    {
            //        TourId = 1,
            //        TourName = "Schoenbrunn",
            //        TourInfo = new TourInfo { To = "Wien" },
            //        TourLogs = schoenbrunnLogs
            //    });

            //}

            //for (int i = 0; i < 5; i++)
            //{
            //    Tours.Add(new Tour
            //    {
            //        TourId = 2,
            //        TourName = "Kahlenberg"
            //    });

            //}


            //foreach(Tour t in Tours)
            //{
            //    var tp = new TourProcessor();

            //    tp.AddTour(t);
            //}
            
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void ButtonMinimize_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void WindowStateButton_Click(object sender, RoutedEventArgs e)
        {
            if(Application.Current.MainWindow.WindowState != WindowState.Maximized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;

            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
