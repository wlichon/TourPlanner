using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using TourPlanner.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using TourPlanner.MVVM.ViewModel;
using TourPlanner.Core;

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

            ObservableCollection<Tour>? tours = null;
            
            (tours, string loadMessage) = await tp.LoadTours();

            if(tours == null)
            {
                MessageBox.Show(loadMessage);
                return;
            }
                
            

            mainViewModel.Tours.Clear();
            foreach (var tour in tours)
            {
                mainViewModel.Tours.Add(tour);
            }
            mainViewModel.FilteredTours = mainViewModel.Tours;
            MessageBox.Show(loadMessage);
            
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
