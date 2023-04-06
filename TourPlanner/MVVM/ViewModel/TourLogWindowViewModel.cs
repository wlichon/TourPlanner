using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using TourPlanner.Models;
using TourPlanner.Core;
using System.ComponentModel;

namespace TourPlanner.MVVM.ViewModel
{
    public class TourLogWindowViewModel : ObservableObject, IDataErrorInfo
    {
        public string Error { get { return null; } }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public string this[string propName]
        {

            get
            {
           
                string result = null;

                switch (propName)
                {
                    case "StartHours":
                        if (StartHours < 0 || StartHours > 23)
                        {
                            result = "The starting hour has to be between 0 and 23 inclusive";
                        }
                        break;
                    case "StartMinutes":
                        if (StartMinutes > 59 || StartMinutes < 0)
                        {
                            result = "The starting minutes have to be between 0 and 59 inclusive";
                        }
                        break;
                    case "DurationMinutes":
                        if (DurationMinutes > 59 || DurationMinutes < 0)
                        {
                            result = "The duration minutes have to be between 0 and 59 inclusive";
                        }
                        break;
                    case "DurationSeconds":
                        if (DurationSeconds > 59 || DurationSeconds < 0)
                        {
                            result = "The duration seconds have to be between 0 and 59 inclusive";
                        }
                        break;
                    case "Comment":
                        if (string.IsNullOrEmpty(Comment))
                        {
                            result = "Comment field cannot be empty";
                        }
                        break;

                }

                if (ErrorCollection.ContainsKey(propName))
                {
                    if (result == null)
                    {
                        ErrorCollection.Remove(propName);
                        return null;
                    }
                    ErrorCollection[propName] = result;

                }
                else if (result != null)
                    ErrorCollection.Add(propName, result);

                if (ErrorCollection.Count == 0)
                {
                    IsAddButtonEnabled = true;
                }
                else
                {
                    IsAddButtonEnabled = false;
                }

                OnPropertyChanged(nameof(ErrorCollection));

                return result;
            }
        }


        private bool _isAddButtonEnabled;
        public bool IsAddButtonEnabled { 
            get { return _isAddButtonEnabled; }
            set
            {
                _isAddButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        private TourLog _tourLog;

        public TourLog TourLog { get { return _tourLog; } set { _tourLog = value;  } }

        /*
        public string? Comment
        {
            get { return _tourLog.Comment; }
            set { _tourLog.Comment = value; }
        }
        */

        public DateTime Date
        {
            get { return _tourLog.Date; }
            set { 
                _tourLog.Date = value;
                OnPropertyChanged(nameof(StartMinutes));
                OnPropertyChanged(nameof(StartHours));

            }
        }
        public ComboBoxItem SelectedDifficultyItem
        {
            set
            {
                _tourLog.Difficulty = Int32.Parse(value.Tag.ToString());
            }
        }
        

        public ComboBoxItem SelectedRatingItem {
          
            set {
                _tourLog.Rating = Int32.Parse(value.Tag.ToString());
            }
        }

        public string Comment
        {
            get { return _tourLog.Comment; }
            set
            {
                _tourLog.Comment = value;
                OnPropertyChanged();
            }
        }


        public double StartMinutes
        {
            get { return _tourLog.Date.Minute; }
            set { 
                if(value < 60 && value >= 0)
                {
                    _tourLog.Date = new DateTime(_tourLog.Date.Year, _tourLog.Date.Month, _tourLog.Date.Day, _tourLog.Date.Hour, Int32.Parse(value.ToString()), 0);
                    OnPropertyChanged();

                }
            }
        }

        public double StartHours
        {
            get { return _tourLog.Date.Hour; }
            set {
                if (value < 24 && value >= 0)
                {
                    _tourLog.Date = new DateTime(_tourLog.Date.Year, _tourLog.Date.Month, _tourLog.Date.Day, Int32.Parse(value.ToString()), _tourLog.Date.Minute, 0);
                    OnPropertyChanged();

                }

            }
        }

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get { return _dialogResult; }
            set { 
                _dialogResult = value;
                OnPropertyChanged();
            }
        }

        private int _durationHours;

        public int DurationHours
        {
            get { return _durationHours; }
            set { 
                _durationHours = value;
                OnPropertyChanged();

            }
        }

        private int _durationMinutes;

        public int DurationMinutes
        {
            get { return _durationMinutes; }
            set { 
                _durationMinutes = value;
                OnPropertyChanged();

            }
        }

        private int _durationSeconds;

        public int DurationSeconds
        {
            get { return _durationSeconds; }
            set { 
                _durationSeconds = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand AddTourLogButton { get; set; }

        /*
        */

 

        /*
        public TimeSpan? Duration
        {
            get
            {
                return _tourLog.Duration;
            }
            set
            {
                _tourLog.Duration = value;
            }
        }
        */
        public TourLogWindowViewModel(TourLog? log)
        {
            IsAddButtonEnabled = false;
            if(log == null)
            {
                _tourLog = new TourLog();
                _tourLog.Date = DateTime.Now;
                _tourLog.Duration = TimeSpan.Zero;
            }
            else
            {
                TourLog = log;
            }

            AddTourLogButton = new RelayCommand(o =>
            {
                TourLog.Duration = new TimeSpan(DurationHours, DurationMinutes, DurationSeconds);
                DialogResult = true;

            });
        }
    }
}
