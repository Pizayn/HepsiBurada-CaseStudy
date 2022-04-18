using Shopping.Aggregator.Models;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public interface IOrderService
    {
        Task<OrderModel> CreateOrder(OrderModel model);
    }
}
