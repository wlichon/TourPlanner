using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models.Models
{
    public class TourProcessor
    {
        public async Task<ObservableCollection<Tour>> LoadTours()
        {
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(ApiHelper.ApiClient.BaseAddress + $"api/tour"))
            {

                ObservableCollection<Tour>? tours = null;
                if (response.IsSuccessStatusCode)
                {
                    tours = await response.Content.ReadFromJsonAsync<ObservableCollection<Tour>>();
                }

                return tours;
            }
        }

        public async Task<bool> LoadTour(int? tourId)
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(
                $"api/tour/{tourId}");

            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AddTour(Tour? tour)
        {
            //string json = @"{
            //  ""tourId"": 0,
            //  ""tourName"": ""Testing"",
            //  ""tourInfo"": {
            //    ""tourInfoId"": 0,
            //    ""from"": ""Bla"",
            //    ""to"": ""asd"",
            //    ""distance"": 0,
            //    ""description"": ""string"",
            //    ""transportType"": ""string"",
            //    ""estimatedTime"": 0
            //  },
            //  ""tourLogs"": [
            //    {
            //      ""tourLogId"": 0,
            //      ""date"": ""2023-03-20T17:24:00.344Z"",
            //      ""duration"": ""1.12:54:56"",
            //      ""distance"": 0,
            //      ""tourId"": 0
            //    }
            //  ]
            //}";
            var json = JsonConvert.SerializeObject(tour);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await ApiHelper.ApiClient.PostAsync("https://localhost:7136/api/tour", data);

            //HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(
            //    $"api/tour", tour);
            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }
        public async Task<bool> UpdateTour(Tour? tour)
        {
            
            HttpResponseMessage response = await ApiHelper.ApiClient.PutAsJsonAsync(
                $"api/tour", tour);
            response.EnsureSuccessStatusCode();

            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;



        }

        public async Task<bool> DeleteTour(int? tourId)
        {
            HttpResponseMessage response = await ApiHelper.ApiClient.DeleteAsync(
                $"api/tour/{tourId}");

            try
            {
                response.EnsureSuccessStatusCode();

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
