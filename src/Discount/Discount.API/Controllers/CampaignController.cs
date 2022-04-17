using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public CampaignController(IDiscountRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{productCode}", Name = "GetCampaign")]
        [ProducesResponseType(typeof(Campaign), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Campaign>> GetCampaign(string productCode)
        {
            var campaign = await _repository.GetCampaign(productCode);
            return Ok(campaign);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Campaign), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Campaign>> CreateCampaign([FromBody] Campaign campaign)
        {
            await _repository.CreateCampaign(campaign);
            return CreatedAtRoute("GetCampaign", new { productCode = campaign.ProductCode }, campaign);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Campaign), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Campaign>> UpdateCampaign([FromBody] Campaign campaign)
        {
            return Ok(await _repository.UpdateCampaign(campaign));
        }

        [HttpDelete("{productCode}", Name = "DeleteCampaign")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteCampaign(string productName)
        {
            return Ok(await _repository.DeleteCampaign(productName));
        }
    }
}
