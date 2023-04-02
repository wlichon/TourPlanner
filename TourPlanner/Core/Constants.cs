using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Core
{
    public static class Constants
    {
        public const string Key = "qL8PNjia3XSMfgNRG4henvQNAnQGprnW";
        public const string MapQuestBaseUrl = "https://www.mapquestapi.com";
        public const string DbApiAddress = "https://localhost:7136";
        public const int RouteImageWidth = 610;
        public const int RouteImageHeight = 300;
        static public string MapQuestDirectionsSuffix(string? from, string? to)
        {
            return $"/directions/v2/route?key={Key}&from={from}&to={to}";
        }
        static public string MapQuestMapSuffix(string? from, string? to)
        {
            return $"/staticmap/v5/map?start={from}&end={to}&size={RouteImageWidth},{RouteImageHeight}@2x&key={Key}";
        }
    }
}
