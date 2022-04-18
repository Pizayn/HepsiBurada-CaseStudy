using Shopping.Aggregator.Models;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public interface IDiscountService
    {
        Task<CampaignModel> GetCampaign(string productCode);
        Task<CampaignModel> UpdateCampaign(CampaignModel model);
    }
}
