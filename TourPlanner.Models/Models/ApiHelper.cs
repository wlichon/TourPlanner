using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Models.Models
{
    public static class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }
        // can be static, is designed to be threadsafe..
        // analogy: ApiClient property is a browser, requests are new tabs you open.
        // You dont need more than 1 browser, and you can open as many tabs as you want

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri("http://localhost:7136");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
