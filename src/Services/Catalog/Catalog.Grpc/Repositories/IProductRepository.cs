using Catalog.Grpc.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Grpc.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string id);
        Task<Product> GetProductByProductCode(string productCode);

        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string id);


    }
}
