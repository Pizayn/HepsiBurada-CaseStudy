using Catalog.API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public interface ITimeRepository
    {
        Task<Time> GetTime();
        Task<bool> UpdateTime(Time time);
        Task<IEnumerable<Time>> GetTimes();

    }
}
