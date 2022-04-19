using Shopping.Aggregator.Models;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public interface ICatalogService
    {
        Task<ProductModel> GetProductByProductCode(string productCode);
        Task UpdateProduct(ProductModel model);

    }
}
