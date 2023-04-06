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
using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;

namespace TourPlanner.MVVM.ViewModel
{
    public class GeneralViewModel : BaseComponent, IDataErrorInfo
    {
        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string this[string propName]
        {

            get
            {
                if (TextboxesEnabled == false)
                    return null;

                string result = null;

                switch (propName)
                {
                    case "ChangedName":
                        if (string.IsNullOrEmpty(ChangedName))
                        {
                            result = "Your tour name needs at least 1 character";
                        }
                        break;
                    case "ChangedFrom":
                        if (string.IsNullOrEmpty(ChangedFrom))
                        {
                            result = "Enter your starting location in this textbox";
                        }
                        break;
                    case "ChangedTo":
                        if (string.IsNullOrEmpty(ChangedTo))
                        {
                            result = "Enter your destination in this textbox";
                        }
                        break;
                    case "ChangedDescription":
                        if (string.IsNullOrEmpty(ChangedDescription))
                        {
                            result = "Enter your description";
                        }
                        break;
                }

                if (ErrorCollection.ContainsKey(propName))
                {
                    if(result == null)
                    {
                        ErrorCollection.Remove(propName);
                        return null;
                    }
                    ErrorCollection[propName] = result;

                }
                else if (result != null)
                    ErrorCollection.Add(propName, result);

                if(ErrorCollection.Count == 0)
                {
                    IsSaveButtonEnabled = true;
                }
                else
                {
                    IsSaveButtonEnabled = false;
                }

                OnPropertyChanged(nameof(ErrorCollection));

                return result;
            }
        }


        private bool _textboxesEnabled = false;

        private string _buttonText;

        private Tour _selectedTour;

        private TourProcessor _tp;

        private TourInfo _changedInfo;

        private string _oldName;

        public string ChangedName
        {
            get { return _oldName; }
            set { 
                _oldName = value;
                OnPropertyChanged();
            }
        }

        public string? ChangedFrom
        {
            get { return _changedInfo?.From; }
            set { 
                _changedInfo.From = value;
                OnPropertyChanged();
            }
        }

        public string? ChangedTransportType
        {
            get { return _changedInfo?.TransportType; }
            set
            {
                if(_changedInfo != null)
                {
                    _changedInfo.TransportType = value;
                    OnPropertyChanged();

                }
            }
        }
        
        public Tour SelectedTour {
            get { return _selectedTour; }
            set
            {
                if(value != _selectedTour)
                {
                    
                    _selectedTour = value;
                    _changedInfo = new TourInfo();
                    _changedInfo.TourInfoId = _selectedTour.TourInfo.TourInfoId;

                    ChangedTransportType = _selectedTour.TourInfo.TransportType;
                    ChangedName = _selectedTour.TourName;
                    ChangedFrom = _selectedTour.TourInfo.From;
                    ChangedTo = _selectedTour.TourInfo.To;
                    ChangedDescription = _selectedTour.TourInfo.Description;
                 
                    EstimatedTime = _selectedTour.TourInfo.EstimatedTime;
                    Distance = _selectedTour.TourInfo.Distance;
                    
                    OnPropertyChanged();
                }
            }
        }

        public string? ChangedDescription
        {
            get { return _changedInfo?.Description; }
            set
            {
                _changedInfo.Description = value;
                OnPropertyChanged();
            }
        }
        public string? ChangedTo
        {
            get { return _changedInfo?.To; }
            set
            {
                _changedInfo.To = value;
                OnPropertyChanged();
            }
        }

        public float? EstimatedTime
        {
            get { return _changedInfo?.EstimatedTime; }
            set
            {
                _changedInfo.EstimatedTime = value;
                OnPropertyChanged();
            }
        }

        public float? Distance
        {
            get { return _changedInfo?.Distance; }
            set
            {
                _changedInfo.Distance = value;
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
                    if(_textboxesEnabled == true)
                    {
                        ChangedName = ChangedName;
                        ChangedFrom = ChangedFrom;
                    }
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSaveButtonEnabled;

        public bool IsSaveButtonEnabled
        {
            get { return _isSaveButtonEnabled; }
            set {
                
                _isSaveButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        
        private void MediatorSendRefreshImage()
        {
            this._mediator.Notify(this, TourEvent.RefreshRouteImage);
        }
        

        public async Task ToggleButtonAsync()
        { 
            if(SelectedTour == null)
            {
                return;
            }
            
            if (TextboxesEnabled)
            {
                bool imageChanged = false;
                var dp = new DirectionsProcessor();

                if((_selectedTour.TourInfo.From != _changedInfo.From || _selectedTour.TourInfo.To != _changedInfo.To) && (!string.IsNullOrEmpty(_changedInfo.To) && !string.IsNullOrEmpty(_changedInfo.From)))
                {
                    (_changedInfo.ImageData, string loadMapMessage) = await dp.LoadMap(_changedInfo.From, _changedInfo.To);
                    (Distance, EstimatedTime, string loadDirectionsMessage) = await dp.LoadDirections(_changedInfo.From, _changedInfo.To, _changedInfo.TransportType);
                    imageChanged = true;
                    MessageBox.Show("MapApi called since locations changed");
                }

                if(!imageChanged && _selectedTour.TourInfo.TransportType != _changedInfo.TransportType)
                {
                    (Distance, EstimatedTime, string loadDirectionsMessage) = await dp.LoadDirections(_changedInfo.From, _changedInfo.To, _changedInfo.TransportType);
                }

                _selectedTour.TourName = ChangedName;
                
                _selectedTour.TourInfo.TourInfoId = _changedInfo.TourInfoId;
                _selectedTour.TourInfo.Distance = _changedInfo.Distance;
                _selectedTour.TourInfo.Description = _changedInfo.Description;
                _selectedTour.TourInfo.From = _changedInfo.From;
                _selectedTour.TourInfo.To = _changedInfo.To;
                _selectedTour.TourInfo.TransportType = _changedInfo.TransportType;
                _selectedTour.TourInfo.ImageData = _changedInfo.ImageData;
                _selectedTour.TourInfo.EstimatedTime = _changedInfo.EstimatedTime;

                if (imageChanged)
                {
                    MediatorSendRefreshImage();
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

            IsSaveButtonEnabled = true;

            ToggleButtonsCommand = new RelayCommand(o => ToggleButtonAsync());
        }


    }
}
