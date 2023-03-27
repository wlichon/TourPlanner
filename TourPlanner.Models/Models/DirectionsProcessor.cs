using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models.Models
{
    public class DirectionsProcessor
    {
        
        public async Task<(byte[]? jpegMap, string message)> LoadMap(string? location)
        {
            HttpResponseMessage response;
            try
            {
                response = await ApiHelper.ApiClient.GetAsync(
                    $"https://www.mapquestapi.com/staticmap/v5/map?start=Wien&end=Salzburg&size=610,300@2x&key=qL8PNjia3XSMfgNRG4henvQNAnQGprnW");

                response.EnsureSuccessStatusCode();

            }
            catch (HttpRequestException ex)
            {
                return (null, "Map loading connection error");
            }
            catch (Exception ex)
            {
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
                        return (jpegMap, "Map loaded successfully");


                    }
                }

            }
            catch (Exception ex)
            {
                return (null, "Map loading unkown error");
            }


            
        }
    }
}
