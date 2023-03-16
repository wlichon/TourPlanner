using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models.Models
{
    public class TourProcessor
    {
        public async Task<List<Tour>> LoadTours()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(ApiHelper.ApiClient.BaseAddress))
            {

                List<Tour>? tours = null;
                if (response.IsSuccessStatusCode)
                {
                    tours = await response.Content.ReadFromJsonAsync<List<Tour>>();
                }

                return tours;
            }
        }

        public async Task<Tour> AddTour(Tour? tour)
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(
                $"api/Tour", tour);
            response.EnsureSuccessStatusCode();

            tour = await response.Content.ReadFromJsonAsync<Tour>();
            return tour;
        }
        public async Task<Tour> UpdateTour(Tour? tour)
        {
            
            HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(
                $"api/Tour", tour);
            response.EnsureSuccessStatusCode();

            tour = await response.Content.ReadFromJsonAsync<Tour>();
            return tour;

            

        }
    }
}
