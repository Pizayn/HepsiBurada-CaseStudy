using Discount.Grpc.Models;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public interface ITimeService
    {
        Task<TimeModel> GetTime();

    }
}
