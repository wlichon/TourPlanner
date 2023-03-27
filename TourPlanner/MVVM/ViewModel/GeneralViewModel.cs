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
using TourPlanner.Models.Models;
using System.Windows.Input;
using System.Windows;
using System.Drawing;
using System.IO;

namespace TourPlanner.MVVM.ViewModel
{
    public class GeneralViewModel : ObservableObject
    {
        private bool _textboxesEnabled = false;

        private string _buttonText;

        private Tour _selectedTour;

        private bool _selectedTourHasChanged = false;

        private TourProcessor _tp;

        private Tour _oldTour;

        public string FormTourNameField { get; set; }

        public string FormDescriptionField { get; set; }
        public string FormFromField { get; set; }

        public string FormToField { get; set; }

        public Tour SelectedTour {
            get { return _selectedTour; }
            set
            {
                if(value != _selectedTour)
                {


                    _selectedTour = value;
                    //_oldTour = (Tour)_selectedTour.Clone();
                    _selectedTourHasChanged = true;
                    OnPropertyChanged();
                }
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

        
        
        

        public async Task ToggleButtonAsync()
        {
            
            if (TextboxesEnabled)
            {
                if (_selectedTourHasChanged)
                {
                    _selectedTourHasChanged = false;
                    int? selectedTourId = _selectedTour.TourId;

                    var dp = new DirectionsProcessor();

                    (byte[]? jpeg, string? message) = await dp.LoadMap("placeholder");

                    _selectedTour.TourInfo.ImageData = jpeg;

                    dp.ConvertArrayToBitmap(jpeg);

                    (bool success, string updateMessage) = await _tp.UpdateTour(_selectedTour);
                    if (success)
                    {
                        MessageBox.Show(updateMessage);
                    }
                    else
                    {
                        //_selectedTour = (Tour)_oldTour.Clone();
                        MessageBox.Show(updateMessage);
                    }
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
