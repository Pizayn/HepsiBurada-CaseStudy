using Catalog.Grpc.Entities;
using MongoDB.Driver;

namespace Catalog.Grpc.Data
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
