using System.Threading.Tasks;
using TimeService.API.Entities;

namespace TimeService.API.Repositories
{
    public interface ITimeRepository
    {
        Task<Time> GetTime();
        Task<bool> UpdateTime(Time time);
    }
}
