using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Rentler.Interview.Client.Models;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text;

namespace Rentler.Interview.Client
{
    public static class FoodApi
    {
        public const string ApiUrl = "http://localhost:7263/food";

        public static async Task<IEnumerable<Food>> GetFood()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetFromJsonAsync<HttpResponseMessage>(ApiUrl);
                return response.Content as IEnumerable<Food>;
            }
        }

        public static async Task AddFood(Food food)
        {
            using (var client = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(food);
                var stringContent = new StringContent(content, Encoding.UTF8, "application/json");
                await client.PostAsync(ApiUrl, stringContent);
            }
        }
    }
}
