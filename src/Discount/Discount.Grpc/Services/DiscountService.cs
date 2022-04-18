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
        private readonly ITimeService _timeService;


        public DiscountService(IDiscountRepository repository, IMapper mapper, ILogger<DiscountService> logger, ITimeService timeService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));

        }

        public override async Task<CampaignModel> GetCampaign(GetCampaignRequest request, ServerCallContext context)
        {
            var campaign = await _repository.GetCampaign(request.ProductCode);
            var time = await _timeService.GetTime();
           
            if (campaign == null)
            {
                return new CampaignModel();
               
            }
            _logger.LogInformation("Campaign is retrieved for ProductCode : {productCode}, Amount : {targetSalesCount}", campaign.ProductCode, campaign.TargetSalesCount);
            if (campaign != null && time.Hour > campaign.Duration)
            {
                campaign.Status = 0;
                await _repository.UpdateCampaign(campaign);
                return new CampaignModel();

            }
            var campaignModel = _mapper.Map<CampaignModel>(campaign);
            return campaignModel;
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
            var campaign = _mapper.Map<Campaign>(request.Campaign);
            campaign.Status = campaign.TargetSalesCount > 0 ? 1 : 0; 
            await _repository.UpdateCampaign(campaign);
            _logger.LogInformation("Campaign is successfully updated. ProductCode : {ProductCode}", campaign.ProductCode);

            var couponModel = _mapper.Map<CampaignModel>(campaign);
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
