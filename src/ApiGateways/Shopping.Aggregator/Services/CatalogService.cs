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
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<ProductModel> GetProductByProductCode(string productCode)
        {
            var response = await _client.GetAsync($"/api/v1/Catalog/GetProductByProductCode/{productCode}");
            if (response.ReasonPhrase == "OK")
            {
                return await response.ReadContentAs<ProductModel>();

            }
            return null;
        }

        public async Task UpdateProduct(ProductModel model)
        {
            var modelJson = new StringContent(
        JsonSerializer.Serialize(model),
        Encoding.UTF8,
        Application.Json);
            var response = await _client.PutAsync($"/api/v1/Catalog", modelJson);
            if (response.IsSuccessStatusCode)
                return;
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }

    }
}
