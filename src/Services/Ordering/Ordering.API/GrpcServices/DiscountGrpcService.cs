﻿using Discount.Grpc.Protos;
using System;
using System.Threading.Tasks;
namespace Ordering.API.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService ?? throw new ArgumentNullException(nameof(discountProtoService));
        }

        public async Task<CampaignModel> GetCampaign(string productCode)
        {
            var discountRequest = new GetCampaignRequest { ProductCode = productCode };
            return await _discountProtoService.GetCampaignAsync(discountRequest);
        }

        public async Task<CampaignModel> UpdateCampaign(UpdateCampaignRequest campaign)
        {
            return await _discountProtoService.UpdateCampaignAsync(campaign);
        }
    }
}
