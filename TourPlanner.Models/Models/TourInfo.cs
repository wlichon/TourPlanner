using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;

namespace TourPlanner.Models
{
    public class TourInfo
    {
        public TourInfo() { }
        public TourInfo(string from, string to, float distance, string description, string transportType, int estimatedTime)
        {
            //SetImage(bitmap);
            From = from;
            To = to;
            Distance = distance;
            Description = description;
            TransportType = transportType;
            EstimatedTime = estimatedTime;
        }
        /*
         * 
         * 
         IMPLEMENT LATER

        public Bitmap GetImage()
        {
            if (ImageData == null)
                return null;
            using (var ms = new MemoryStream(ImageData))
            {
                return new Bitmap(ms);
            }
        }

        public void SetImage(Bitmap image)
        {
            using (var ms = new MemoryStream())
            {
                ImageData = ms.ToArray();
            }
        }
        public byte[] ImageData { get; set; }

        */

        public int? TourInfoId { get; set; }
        public string? From { get; 
            set; }
        public string? To { get; set; }
        public float? Distance { get; set; }
        public string? Description { get; set; }
        public string? TransportType { get; set; }
        public float? EstimatedTime{ get; set; }

    }
}
