using AutoMapper;
using Discount.API.Repositories;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IDiscountRepository repository, IMapper mapper, ILogger<DiscountService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<CampaignModel> GetCampaign(GetCampaignRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetCampaign(request.ProductCode);
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Campaign with ProductName={request.ProductCode} is not found."));
            }
            _logger.LogInformation("Campaign is retrieved for ProductCode : {productCode}, Amount : {targetSalesCount}", coupon.ProductCode, coupon.TargetSalesCount);

            var couponModel = _mapper.Map<CampaignModel>(coupon);
            return couponModel;
        }

        public override async Task<CampaignModel> CreateCampaign(CreateCampaignRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Campaign>(request.Campaign);

            await _repository.CreateCampaign(coupon);
            _logger.LogInformation("Discount is successfully created. ProductCode : {ProductCode}", coupon.ProductCode);

            var couponModel = _mapper.Map<CampaignModel>(coupon);
            return couponModel;
        }

        public override async Task<CampaignModel> UpdateCampaign(UpdateCampaignRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Campaign>(request.Campaign);

            await _repository.UpdateCampaign(coupon);
            _logger.LogInformation("Discount is successfully updated. ProductCode : {ProductCode}", coupon.ProductCode);

            var couponModel = _mapper.Map<CampaignModel>(coupon);
            return couponModel;
        }

        public override async Task<DeleteCampaignResponse> DeleteCampaign(DeleteCampaignRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteCampaign(request.ProductCode);
            var response = new DeleteCampaignResponse
            {
                Success = deleted
            };

            return response;
        }
    }
}
