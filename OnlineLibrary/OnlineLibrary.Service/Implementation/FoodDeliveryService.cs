using Newtonsoft.Json;
using OnlineLibrary.Domain.FoodDeliveryDomain;
using OnlineLibrary.Service.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineLibrary.Service.Implementation
{
    public class FoodDeliveryService : IFoodDeliveryService
    {
        private readonly HttpClient _httpClient;
        private readonly string foodDeliveryApiUrl = "https://fooddeliverywebapp211076211032212029.azurewebsites.net/api/";

        public FoodDeliveryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Order>> GetAllActiveOrdersAsync()
        {
            var response = await _httpClient.GetAsync(foodDeliveryApiUrl + "Admin/GetAllActiveOrders");
            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<List<Order>>(jsonData);
                return orders;
            }
            return new List<Order>();
        }
    }
}
