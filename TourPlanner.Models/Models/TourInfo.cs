using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


namespace TourPlanner.Models
{
    public class TourInfo : ICloneable
    {
        public TourInfo() { }
        public TourInfo(string from, string to, float distance, string description, string transportType, int estimatedTime)
        {
            From = from;
            To = to;
            Distance = distance;
            Description = description;
            TransportType = transportType;
            EstimatedTime = estimatedTime;
        }
       
        public byte[]? ImageData { get; set; }

        public int? TourInfoId { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public float? Distance { get; set; }
        public string? Description { get; set; }
        public string? TransportType { get; set; }
        public float? EstimatedTime{ get; set; }

        public object Clone()
        {
            var clonedInfo = new TourInfo
            {
                From = From,
                To = To
            };

            return clonedInfo;
        }
    }
}
