using Discount.API.Extensions;
using Discount.API.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Discount.API.Services
{
    public class TimeService: ITimeService
    {
        private readonly HttpClient _client;

        public TimeService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<TimeModel> GetTime()
        {
            var response = await _client.GetAsync($"/api/v1/Time/");
            return await response.ReadContentAs<TimeModel>();
        }
    }
}
