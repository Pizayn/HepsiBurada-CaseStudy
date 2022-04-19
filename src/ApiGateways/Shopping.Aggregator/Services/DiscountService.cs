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
    public class DiscountService : IDiscountService
    {

        private readonly HttpClient _client;

        public DiscountService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<CampaignModel> GetCampaign(string productCode)
        {
            var response = await _client.GetAsync($"/api/v1/Campaign/{productCode}");
            if (response.ReasonPhrase == "OK")
            {
                return await response.ReadContentAs<CampaignModel>();

            }
            return null;
        }

        public async Task UpdateCampaign(CampaignModel model)
        {
            var modelJson = new StringContent(
        JsonSerializer.Serialize(model),
        Encoding.UTF8,
        Application.Json);
            var response = await _client.PutAsync($"/api/v1/Campaign", modelJson);
            if (response.IsSuccessStatusCode)
                return;
            else
            {
                throw new Exception("Something went wrong when calling api.");
            }
        }
    }
}
