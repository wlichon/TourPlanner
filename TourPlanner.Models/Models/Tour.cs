using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using TourPlanner.Models;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System.Drawing;

namespace TourPlanner.Models
{
    public class Tour : ICloneable
    {
        public Tour()
        {
            //{
            //  "tourId": 0,
            //  "tourName": "string",
            //  "tourInfo": {
            //    "tourInfoId": 0,
            //    "from": "string",
            //    "to": "string",
            //    "distance": 0,
            //    "description": "string",
            //    "transportType": "string",
            //    "estimatedTime": 0
            //  },
            //  "tourLogs": [
            //    {
            //      "tourLogId": 0,
            //      "date": "2023-03-20T22:40:41.312Z",
            //      "duration": "1.12:23:34",
            //      "distance": 0,
            //      "tourId": 0
            //    }
            //  ]
            //}
            
            //TourInfo = new TourInfo { From = "default", To = "default", Distance = 0, Description = "default", TransportType = "default", EstimatedTime = 0 };
            

        }
        public static bool operator ==(Tour obj1, Tour obj2)
        {


            if (ReferenceEquals(obj1, obj2))
            {
                return true;
            }

            if (ReferenceEquals(obj1, null) || ReferenceEquals(obj2, null))
            {
                return false;
            }

            bool a = (obj1.TourName == obj2.TourName);
            bool b = obj1.TourLogs.SequenceEqual(obj2.TourLogs);
            bool c = (obj1.TourInfo.TransportType == obj2.TourInfo.TransportType);
            bool d = (obj1.TourInfo.Distance == obj2.TourInfo.Distance);
            bool e = (obj1.TourInfo.From == obj2.TourInfo.From);
            bool f = (obj1.TourInfo.To == obj2.TourInfo.To);
            bool g = (obj1.TourInfo.Description == obj2.TourInfo.Description);
            bool h = (obj1.TourInfo.EstimatedTime == obj2.TourInfo.EstimatedTime);
            //bool i = (obj1.TourInfo.ImageData == obj2.TourInfo.ImageData);

            return a && b && c && d && e && f && g && h;
        }

        public static bool operator !=(Tour obj1, Tour obj2)
        {
            return !(obj1 == obj2);
        }

        private int? _tourId;
        public int? TourId
        {
            get { return _tourId; }
            set { _tourId = value; }
        }

        private string? _tourName;
        public string TourName
        {
            get { return _tourName; }
            set { _tourName = value; }
        }

        private TourInfo? _tourInfo;

        
        public TourInfo? TourInfo
        {
            get { return _tourInfo; }
            set { _tourInfo = value; }
        }

        private ObservableCollection<TourLog>? _tourLogs = new ObservableCollection<TourLog>();

        public ObservableCollection<TourLog>? TourLogs
        {
            get { return _tourLogs; }
            set { 
                _tourLogs = value;
            }
        }

        public object Clone()
        {
            Tour tourCopy = (Tour)this.MemberwiseClone();
            tourCopy.TourInfo = new TourInfo
            {
                From = this.TourInfo?.From,
                To = this.TourInfo?.To,
                Distance = this.TourInfo.Distance,
                Description = this.TourInfo.Description,
                TransportType = this.TourInfo.TransportType,
                EstimatedTime = this.TourInfo.EstimatedTime
            };

            return tourCopy;
        }
    }
}
