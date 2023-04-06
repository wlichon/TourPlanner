using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Newtonsoft.Json;

namespace TourPlanner.Core
{
    public class DirectionsProcessor
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public async Task<(byte[]? jpegMap, string message)> LoadMap(string? from, string? to)
        {
            HttpResponseMessage response;
            try
            {
                string suffix = Constants.MapQuestMapSuffix(from, to);
                response = await ApiHelper.ApiClient.GetAsync(Constants.MapQuestBaseUrl+suffix);

                response.EnsureSuccessStatusCode();

            }
            catch (HttpRequestException ex)
            {
                log.Info(ex.Message);
                return (null, "Map loading connection error");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return (null, "Map loading unkown error");
            }



            try
            {
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        var jpegMap = memoryStream.ToArray();
                        return (jpegMap, "Map loaded from api");


                    }
                }

            }
            catch (Exception ex)
            {
                log.Info("Map loading unknown error");
                return (null, "Map loading unknown error");
            }


            
        }

        struct DirectionsInfo
        {
            struct Route
            {
                public int distance;
                public int time;

            }
        }

        public async Task<(int distance, int time, string message)> LoadDirections(string? from, string? to, string? transportType)
        {
            HttpResponseMessage response;
            try
            {
                string suffix = Constants.MapQuestDirectionsSuffix(from, to, transportType);
                response = await ApiHelper.ApiClient.GetAsync(Constants.MapQuestBaseUrl + suffix);

                response.EnsureSuccessStatusCode();

            }
            catch (HttpRequestException ex)
            {
                log.Info("Directions loading connection error");
                return (-1, -1, "Directions loading connection error");
            }
            catch (Exception ex)
            {
                log.Info("Directions loading unknown error");
                return (-1, -1, "Directions loading unknown error");
            }

            

            try
            {

                var content = await response.Content.ReadAsStringAsync();

                var info = JsonConvert.DeserializeObject<dynamic>(content);

                //int distance = 
                return (info.route.distance * 1.61, info.route.time / 60, "Distance and Time loaded"); // distance conversion: miles -> AND time conversion: seconds -> minutes


            }
            catch (Exception ex)
            {
                log.Info("Directions loading unknown error");
                return (-1,-1, "Directions loading unknown error");
            }



        }
    }
}
