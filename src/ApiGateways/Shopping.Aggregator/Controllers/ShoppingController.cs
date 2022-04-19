using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        private readonly IDiscountService _discountService;
        private readonly IOrderService _orderService;

        public ShoppingController(ICatalogService catalogService, IDiscountService discountService, IOrderService orderService)
        {
            _catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            _discountService = discountService ?? throw new ArgumentNullException(nameof(discountService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        [HttpPost(Name = "GetShopping")]
        [ProducesResponseType(typeof(OrderModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<int>> GetShopping([FromBody] OrderModel model)
        {
          
            var product = await _catalogService.GetProductByProductCode(model.ProductCode);
            var campaign = await _discountService.GetCampaign(model.ProductCode);

            if(product == null)
            {
                return NotFound("Product is null");
            }
            if(product.Stock < model.Quantity)
            {
                return BadRequest("Stock must be greater than order");
            }
            if(campaign != null && campaign.TargetSalesCount >= model.Quantity)
            {
                campaign.TargetSalesCount -= model.Quantity;
               await _discountService.UpdateCampaign(campaign);
            }
            product.Stock -= model.Quantity;
            await _catalogService.UpdateProduct(product);

            var order = await _orderService.CreateOrder(model);
            return Ok(order);
        }
    }
}
