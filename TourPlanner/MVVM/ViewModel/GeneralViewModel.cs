using TourPlanner.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Windows.Input;
using System.Windows;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace TourPlanner.MVVM.ViewModel
{
    public class GeneralViewModel : BaseComponent
    {
        private bool _textboxesEnabled = false;

        private string _buttonText;

        private Tour _selectedTour;

        private TourProcessor _tp;

        private TourInfo _oldInfo;

        
        public Tour SelectedTour {
            get { return _selectedTour; }
            set
            {
                if(value != _selectedTour)
                {
                    
                    _selectedTour = value;
                    EstimatedTime = _selectedTour.TourInfo.EstimatedTime;
                    Distance = _selectedTour.TourInfo.Distance;
                    _oldInfo = (TourInfo)_selectedTour.TourInfo.Clone();
                    OnPropertyChanged();
                }
            }
        }

        public float? EstimatedTime
        {
            get { return _selectedTour.TourInfo.EstimatedTime; }
            set
            {
                _selectedTour.TourInfo.EstimatedTime = value;
                OnPropertyChanged();
            }
        }

        public float? Distance
        {
            get { return _selectedTour.TourInfo.Distance; }
            set
            {
                _selectedTour.TourInfo.Distance = value;
                OnPropertyChanged();
            }
        }

        public string ButtonText
        {
            get { return _buttonText; }
            set 
            { 

                if(value != _buttonText)
                {
                    _buttonText = value;
                    OnPropertyChanged();

                }

            }
        }



        public RelayCommand ToggleButtonsCommand { get; set; }
        public bool TextboxesEnabled
        {
            get { return _textboxesEnabled; }
            set
            {
                if(_textboxesEnabled != value)
                {
                    _textboxesEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        
        private void MediatorSendRefreshImage()
        {
            this._mediator.Notify(this, TourEvent.RefreshRouteImage);
        }
        

        public async Task ToggleButtonAsync()
        {
            
            if (TextboxesEnabled)
            {
                if(_selectedTour.TourInfo.From != _oldInfo.From || _selectedTour.TourInfo.To != _oldInfo.To)
                {
                    var dp = new DirectionsProcessor();
                    (_selectedTour.TourInfo.ImageData, string loadMapMessage) = await dp.LoadMap(_selectedTour.TourInfo.From, _selectedTour.TourInfo.To);
                    (Distance, EstimatedTime, string loadDirectionsMessage) = await dp.LoadDirections(_selectedTour.TourInfo.From, _selectedTour.TourInfo.To);
                    MediatorSendRefreshImage();
                    MessageBox.Show("MapApi called since locations changed");
                }


                (bool success, string updateMessage) = await _tp.UpdateTour(_selectedTour);
                if (success)
                {
                    MessageBox.Show(updateMessage);
                }
                else
                {
                       
                    MessageBox.Show(updateMessage);
                }
                
                ButtonText = "Edit";
            }

            else
            {
                ButtonText = "Save";

            }

            TextboxesEnabled = !TextboxesEnabled;

            
        }
        public GeneralViewModel()
        {
            _tp = new TourProcessor();

            ButtonText = "Edit";

            ToggleButtonsCommand = new RelayCommand(o => ToggleButtonAsync());
        }


    }
}
