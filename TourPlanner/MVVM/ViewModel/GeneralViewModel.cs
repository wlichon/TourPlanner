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

namespace TourPlanner.MVVM.ViewModel
{
    internal class GeneralViewModel : ObservableObject
    {
        private bool _textboxesEnabled = false;

        private string _buttonText;

        private Tour _selectedTour;

        private bool _selectedTourHasChanged = false;

        private TourProcessor _tp;

        public Tour SelectedTour {
            get { return _selectedTour; }
            set
            {
                if(value != _selectedTour)
                {
                    _selectedTour = value;
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
                    Tour response = await _tp.UpdateTour(_selectedTour);
                    Console.WriteLine(response.TourName);
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

            ButtonText = "Edit";

            ToggleButtonsCommand = new RelayCommand(o => ToggleButtonAsync());
        }


    }
}
