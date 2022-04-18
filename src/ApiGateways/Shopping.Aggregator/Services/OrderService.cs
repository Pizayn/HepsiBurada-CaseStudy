using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
namespace Shopping.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<OrderModel> CreateOrder(OrderModel model)
        {
            var modelJson = new StringContent(
        JsonSerializer.Serialize(model),
        Encoding.UTF8,
        Application.Json);
            var response = await _client.PostAsync($"/api/v1/Order", modelJson);
            if (response.IsSuccessStatusCode)
                return await response.ReadContentAs<OrderModel>();
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
