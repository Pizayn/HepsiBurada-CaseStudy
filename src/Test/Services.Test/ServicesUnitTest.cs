using Catalog.API.Entities;
using Catalog.API.Repositories;
using System;
using System.Net;
using System.Net.Http;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Discount.API.Entities;
using Ordering.Domain.Entities;

namespace Services.Test
{
    public class ServicesUnitTest
    {
        private readonly HttpClient _client;

        public ServicesUnitTest(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }
        [Fact]
        public async Task GetProductByProductCode()
        {
            _client.BaseAddress = new Uri("http://localhost:5000");

            var productCode = "P1";
            var response = await _client.GetAsync($"/api/v1/Catalog/GetProductByProductCode/{productCode}");

            Assert.Equal("OK", response.ReasonPhrase);

                  

        }
        [Fact]
        public async Task UpdateProduct()
        {
            _client.BaseAddress = new Uri("http://localhost:5000");
            var product = new Product();
            product.Stock = 100;
            product.Price = 100;
            product.ProductCode = "P1";

            var modelJson = new StringContent(
       JsonSerializer.Serialize(product),
       Encoding.UTF8,
       Application.Json);
            var response = await _client.PutAsync($"/api/v1/Catalog", modelJson);
            Assert.Equal("OK", response.ReasonPhrase);

        }
        public async Task GetCampaign()
        {
            _client.BaseAddress = new Uri("http://localhost:5002");

            var productCode = "P1";
            var response = await _client.GetAsync($"/api/v1/Campaign/{productCode}");

            Assert.Equal("OK", response.ReasonPhrase);



        }
        [Fact]
        public async Task UpdateCampaign()
        {
            _client.BaseAddress = new Uri("http://localhost:5002");
            var campaign = new Campaign();
            campaign.Status = 1;
            campaign.Name = "C1";

            var modelJson = new StringContent(
       JsonSerializer.Serialize(campaign),
       Encoding.UTF8,
       Application.Json);
            var response = await _client.PutAsync($"/api/v1/Campaign", modelJson);
            Assert.Equal("OK", response.ReasonPhrase);

        }
        [Fact]

        public async Task CreateOrder()
        {
            _client.BaseAddress = new Uri("http://localhost:5004");
            var order = new Order();
            order.Quantity = 100;
            order.ProductCode = "P1";

            var modelJson = new StringContent(
        JsonSerializer.Serialize(order),
        Encoding.UTF8,
        Application.Json);
            var response = await _client.PostAsync($"/api/v1/Order", modelJson);
            Assert.Equal("OK", response.ReasonPhrase);

        }


    }
}
