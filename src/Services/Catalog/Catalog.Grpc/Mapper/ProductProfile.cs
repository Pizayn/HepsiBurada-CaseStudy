using AutoMapper;
using Catalog.Grpc.Entities;
using Catalog.Grpc.Protos;

namespace Catalog.Grpc.Mapper
{
    public class ProductProfile: Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>().ReverseMap();
        }
    }
}
