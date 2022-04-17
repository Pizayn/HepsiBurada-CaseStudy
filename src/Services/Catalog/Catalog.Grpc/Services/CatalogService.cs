using AutoMapper;
using Catalog.Grpc.Entities;
using Catalog.Grpc.Protos;
using Catalog.Grpc.Repositories;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Catalog.Grpc.Services
{
    public class CatalogService : CatalogProtoService.CatalogProtoServiceBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CatalogService> _logger;

        public CatalogService(IProductRepository repository, IMapper mapper, ILogger<CatalogService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<ProductModel> GetProduct(GetProductRequest request, ServerCallContext context)
        {
            var product = await _repository.GetProductByProductCode(request.ProductCode);
            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Product with ProductCode={request.ProductCode} is not found."));
            }
            _logger.LogInformation("Product is retrieved for ProductCode : {productCode}, Price : {Price}", product.ProductCode, product.Price);

            var productModel = _mapper.Map<ProductModel>(product);
            return productModel;
        }

        public override async Task<ProductModel> UpdateProduct(UpdateProductRequest request, ServerCallContext context)
        {
            var product = _mapper.Map<Product>(request.Product);

            await _repository.UpdateProduct(product);
            _logger.LogInformation("Product is successfully updated. ProductCode : {ProductCode}", product.ProductCode);

            var couponModel = _mapper.Map<ProductModel>(product);
            return couponModel;
        }
    }
}
