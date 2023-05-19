using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Core
{
    public static class Constants
    {
        static public string MapQuestDirectionsSuffix(string? from, string? to, string? transportType)
        {
            return $"/directions/v2/route?key={ConfigurationManager.AppSettings["ApiKey"]}&from={from}&to={to}&routeType={transportType}";
        }
        static public string MapQuestMapSuffix(string? from, string? to)
        {
            return $"/staticmap/v5/map?start={from}&end={to}&size={ConfigurationManager.AppSettings["ImageWidth"]},{ConfigurationManager.AppSettings["ImageHeight"]}@2x&key={ConfigurationManager.AppSettings["ApiKey"]}";
        }
    }
}
