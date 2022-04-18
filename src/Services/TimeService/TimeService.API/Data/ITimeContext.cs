using MongoDB.Driver;
using TimeService.API.Entities;

namespace TimeService.API.Data
{
    public interface ITimeContext
    {
        IMongoCollection<Time> Times { get; }

    }
}
