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
        public async Task<(ObservableCollection<Tour>? tours, string message)> LoadTours()
        {
            ObservableCollection<Tour>? tours = null;
            try
            {

                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(ApiHelper.ApiClient.BaseAddress + $"api/tour"))
                {

                    
                    if (response.IsSuccessStatusCode)
                    {
                    
                        tours = await response.Content.ReadFromJsonAsync<ObservableCollection<Tour>>();

                    
                    
                    }
                

                    return (tours, "Tours loaded successfully");
                }

            }
            catch (HttpRequestException ex)
            {
                return (tours, "Tours loaded unsuccessfully");
            }
        }

        public async Task<(bool success, string message)> LoadTour(int? tourId)
        {
            HttpResponseMessage response;
            try
            {
                response = await ApiHelper.ApiClient.GetAsync(
                    $"api/tour/{tourId}");

                response.EnsureSuccessStatusCode();

            }
            catch(HttpRequestException ex)
            {
                return (false, "Tour loading connection error");
            }
            catch(Exception ex)
            {
                return (false, "Tour loading unkown error");
            }

            
            

            return (true, "Tour loaded successfully");
        }

        public async Task<(bool success, string message)> AddTour(Tour? tour)
        {
            
            var json = JsonConvert.SerializeObject(tour);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response;
            try
            {
                response = await ApiHelper.ApiClient.PostAsync("https://localhost:7136/api/tour", data);
                response.EnsureSuccessStatusCode();


            }
            catch (HttpRequestException ex)
            {
                return (false, "Tour adding connection error");
            }
            catch (Exception ex)
            {
                return (false, "Tour adding unknown error");
            }

            return (true, "Tour added successfully");

        }
        public async Task<(bool success, string message)> UpdateTour(Tour? tour)
        {


            HttpResponseMessage response;

            try
            {
                response = await ApiHelper.ApiClient.PutAsJsonAsync(
                $"api/tour", tour);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                return (false, "Update connection error");
            }
            catch (Exception ex)
            {
                return (false, "Update unknown error");
            }

            return (true, "Update successful");



        }

        public async Task<(bool success, string message)> DeleteTour(int? tourId)
        {
            HttpResponseMessage response;

            try
            {

                response = await ApiHelper.ApiClient.DeleteAsync(
                $"api/tour/{tourId}");
                response.EnsureSuccessStatusCode();

            }
            catch(HttpRequestException ex)
            {
                return (false, "Delete connection error");
            }
            catch (Exception ex)
            {
                return (false, "Delete unknown error");
            }

            return (true, "Deleted successfully");
        }
    }
}
