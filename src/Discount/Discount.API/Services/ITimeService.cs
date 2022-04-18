using Discount.API.Models;
using System.Threading.Tasks;

namespace Discount.API.Services
{
    public interface ITimeService
    {
        Task<TimeModel> GetTime();

    }
}
