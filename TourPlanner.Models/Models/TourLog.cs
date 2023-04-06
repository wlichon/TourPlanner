using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models
{
    public class TourLog : ICloneable, INotifyPropertyChanged
    {

        public object Clone()
        {
            var clonedLog = new TourLog
            {
                Date = Date,
                Comment = Comment,
                Difficulty = Difficulty,
                Duration = Duration,
                Rating = Rating
            };

            return clonedLog;
        }
        public int TourLogId { get; set; }

        private DateTime _date;

        public DateTime Date
        {
            get { return _date; }
            set { 
                _date = value;
                OnPropertyChanged();
            }
        }

        private string? _comment;

        public string? Comment
        {
            get { return _comment; }
            set { 
                _comment = value;
                OnPropertyChanged();
            }
        }

        private int? _difficulty;

        public int? Difficulty
        {
            get { return _difficulty; }
            set { 
                _difficulty = value;
                OnPropertyChanged();
            }
        }



        private TimeSpan? _duration;

        public TimeSpan? Duration
        {
            get { return _duration; }
            set { 
                _duration = value;
                OnPropertyChanged();
            }
        }

        private int? _rating;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public int? Rating
        {
            get { return _rating; }
            set { 
                _rating = value;
                OnPropertyChanged();
            }
        }

        public int TourId { get; set; }




    }
}
