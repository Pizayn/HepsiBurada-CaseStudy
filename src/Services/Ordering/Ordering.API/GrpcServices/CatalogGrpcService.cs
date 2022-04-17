using Catalog.Grpc.Protos;
using System;
using System.Threading.Tasks;

namespace Ordering.API.GrpcServices
{
    public class CatalogGrpcService
    {
        private readonly CatalogProtoService.CatalogProtoServiceClient _catalogProtoService;

        public CatalogGrpcService(CatalogProtoService.CatalogProtoServiceClient catalogProtoService)
        {
            _catalogProtoService = catalogProtoService ?? throw new ArgumentNullException(nameof(catalogProtoService));
        }

        public async Task<ProductModel> GetProduct(string productCode)
        {
            var discountRequest = new GetProductRequest { ProductCode = productCode };
            return await _catalogProtoService.GetProductAsync(discountRequest);
        }
    }
}
