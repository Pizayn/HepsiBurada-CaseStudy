using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using TimeService.API.Entities;

namespace TimeService.API.Data
{
    public class TimeContext : ITimeContext
    {
        public TimeContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Times = database.GetCollection<Time>(configuration.GetValue<string>("DatabaseSettings:CollectionNameTime"));

        }
        public IMongoCollection<Time> Times { get; }

    }
}
