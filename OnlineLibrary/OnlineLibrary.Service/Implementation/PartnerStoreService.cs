using Newtonsoft.Json;
using OnlineLibrary.Domain.Domain;
using OnlineLibrary.Domain.FoodDeliveryDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Service.Implementation
{
    public class PartnerStoreService
    {
        private readonly HttpClient _httpClient;

        public PartnerStoreService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Order>> GetActiveOrdersAsync()
        {
            var response = await _httpClient.GetStringAsync("GetAllActiveOrders");
            return JsonConvert.DeserializeObject<List<Order>>(response);
        }

        public async Task<Order> GetOrderDetailsAsync(Domain.FoodDeliveryDomain.BaseEntity model)
        {
            var response = await _httpClient.PostAsJsonAsync("GetDetailsForOrder", model);
            return await response.Content.ReadFromJsonAsync<Order>();
        }
    }
}
