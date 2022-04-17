using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


        public OrderController(IMediator mediator, DiscountGrpcService discountGrpcService, CatalogGrpcService catalogGrpcService)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _discountGrpcService = discountGrpcService;
            _catalogGrpcService = catalogGrpcService;

        }



        [HttpPost(Name = "CreateOrder")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var campaign = await _discountGrpcService.GetCampaign("P2");
            var product = await _catalogGrpcService.GetProduct("P2");
            var result = await _mediator.Send(command);
            return Ok(result);
        }

     
       
    }
}
