using TourPlanner.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.MVVM.ViewModel
{
    internal class GeneralViewModel : ObservableObject
    {
        private bool _textboxesEnabled = false;

        private string _buttonText;

        private Tour _selectedTour;


        public Tour SelectedTour { 
            get { return _selectedTour; }
            set
            {
                if(value != _selectedTour)
                {
                    _selectedTour = value;
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
        public GeneralViewModel()
        {
            ButtonText = "Edit";

            ToggleButtonsCommand = new RelayCommand(o =>
            {
                if (TextboxesEnabled)
                {
                    ButtonText = "Edit";
                }
                else
                {
                    ButtonText = "Save";

                }

                TextboxesEnabled = !TextboxesEnabled;

            });
        }


    }
}
