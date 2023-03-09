using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.MVVM.Model
{
    internal class TourInfo
    {
        public string Image { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Distance { get; set; }
        public string Description { get; set; }
        public string TransportType { get; set; }
        public string EstimatedTime{ get; set; }

    }
}
