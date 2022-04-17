using Catalog.Grpc.Protos;
using Discount.Grpc.Protos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ordering.API.GrpcServices;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace Ordering.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly DiscountGrpcService _discountGrpcService;
        private readonly CatalogGrpcService _catalogGrpcService;
        private readonly ILogger<OrderController> _logger;


        public OrderController(IMediator mediator, DiscountGrpcService discountGrpcService, CatalogGrpcService catalogGrpcService, ILogger<OrderController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _discountGrpcService = discountGrpcService;
            _catalogGrpcService = catalogGrpcService;
            _logger = logger;
        }



        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var campaign = await _discountGrpcService.GetCampaign(command.ProductCode);
            var product = await _catalogGrpcService.GetProduct(command.ProductCode);
            if (product == null)
            {
                return NotFound();
            }
            if (campaign != null)
            {
                if(campaign.TargetSalesCount < command.Quantity)
                {
                    _logger.LogError($"Target sales count must be greater than order quantity ");
                    return BadRequest();
                }

                campaign.TargetSalesCount -= command.Quantity;
                UpdateCampaignRequest updateCampaignRequest = new UpdateCampaignRequest();
                updateCampaignRequest.Campaign = campaign;
                await _discountGrpcService.UpdateCampaign(updateCampaignRequest);
            }

            if (product.Stock < command.Quantity)
            {
                _logger.LogError($"Product stock must be greater than order quantity ");
                return BadRequest();
            }
            product.Stock -= command.Quantity;
            UpdateProductRequest updateProductRequest = new UpdateProductRequest();
            updateProductRequest.Product = product;
            await _catalogGrpcService.UpdateProduct(updateProductRequest);





            var result = await _mediator.Send(command);
            return Ok(command);
        }

     
       
    }
}
