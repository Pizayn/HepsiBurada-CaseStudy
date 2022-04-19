using Discount.Grpc.Entities;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public interface IDiscountRepository
    {
        Task<Campaign> GetCampaign(string productCode);

        Task<bool> CreateCampaign(Campaign campaign);
        Task<bool> UpdateCampaign(Campaign campaign);
        Task<bool> DeleteCampaign(string productCode);
    }
}
