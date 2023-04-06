using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Models;

namespace TourPlanner.Core
{
    public class TourProcessor
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public async Task<(ObservableCollection<Tour>? tours, string message)> LoadTours()
        {
            ObservableCollection<Tour>? tours = null;
            try
            {

                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync($"api/tour"))
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
                log.Info("The database api is refusing your request, likely due to it being offline");
                return (tours, "The database api is refusing your request, likely due to it being offline");
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
                log.Info(ex.Message);
                return (false, ex.Message);
            }
            catch(Exception ex)
            {
                log.Info(ex.Message);
                return (false, ex.Message);
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
                response = await ApiHelper.ApiClient.PostAsync("/api/tour", data);
                response.EnsureSuccessStatusCode();


            }
            catch (HttpRequestException ex)
            {
                log.Info("Tour cannot be added, due to the api database refusing your request");
                return (false, "Tour cannot be added, due to the api database refusing your request");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return (false, ex.Message);
            }

            return (true, "Tour added successfully");

        }
        public async Task<(bool success, string message)> UpdateTour(Tour? tour)
        {


            HttpResponseMessage response;

            try
            {
                var json = JsonConvert.SerializeObject(tour);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                response = await ApiHelper.ApiClient.PutAsync(
                $"/api/tour", data);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException ex)
            {
                log.Info("Tour cannot be updated, due to the api database refusing your request");
                return (false, "Tour cannot be added, due to the api database refusing your request");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return (false, ex.Message);
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
                log.Info("Tour cannot be deleted, due to the api database refusing your request");
                return (false, "Tour cannot be added, due to the api database refusing your request");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return (false, ex.Message);
            }

            return (true, "Deleted tour successfully");
        }

        public async Task<(bool success, string message)> DeleteTourLog(int? tourLogId)
        {
            HttpResponseMessage response;

            try
            {

                response = await ApiHelper.ApiClient.DeleteAsync(
                $"api/tour/log/{tourLogId}");
                response.EnsureSuccessStatusCode();

            }
            catch (HttpRequestException ex)
            {
                log.Info("Tour log cannot be added, due to the api database refusing your request");
                return (false, "Tour log cannot be added, due to the api database refusing your request");
            }
            catch (Exception ex)
            {
                log.Info(ex.Message);
                return (false, ex.Message);
            }

            return (true, "Deleted log successfully");
        }
    }
}
