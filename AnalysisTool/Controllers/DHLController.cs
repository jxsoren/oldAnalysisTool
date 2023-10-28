using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AnalysisTool.Controllers
{
    public class DHLController : Controller
    {
        public string token;
        public string link;
        public string manifestData;
        public string pickupLocation;

        public void RequestToken(string clientId, string clientSecret, string pickup)
        {
            pickupLocation = pickup;

            string endPoint = "https://api.dhlecs.com/";
            var client = new HttpClient();
            client.BaseAddress = new Uri(endPoint);

            var request = new HttpRequestMessage(HttpMethod.Post, "auth/v4/accesstoken");

            request.Content = new StringContent("application/x-www-form-urlencoded");


            //request.Content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var data = new[]
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
            };
            
            //var result = client.PostAsync(endPoint, new FormUrlEncodedContent(data)).GetAwaiter().GetResult();
            var result = client.SendAsync(request).Result;

            Console.WriteLine(result.Content.ReadAsStringAsync());
            Console.WriteLine("end");
        }

        public void CreateManifest(string clientId, string clientSecret)
        {
            string endPoint = "https://api.dhlecs.com/auth/v4/accesstoken";
            var client = new HttpClient();

            var data = new[]
            {
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
            };

            var result = client.PostAsync(endPoint, new FormUrlEncodedContent(data)).GetAwaiter().GetResult();
        }
    }
}
