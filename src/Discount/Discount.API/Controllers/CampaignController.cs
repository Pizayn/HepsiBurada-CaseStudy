using Discount.API.Entities;
using Discount.API.Repositories;
using Discount.API.Services;
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
        private readonly ITimeService _timeService;

        public CampaignController(IDiscountRepository repository, ITimeService timeService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _timeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
        }

        [HttpGet("{productCode}", Name = "GetCampaign")]
        [ProducesResponseType(typeof(Campaign), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Campaign>> GetCampaign(string productCode)
        {
            var time = await _timeService.GetTime();
            var campaign = await _repository.GetCampaign(productCode);
            if(time.Hour > campaign.Duration)
            {
                campaign.Status = 0;
               await  _repository.UpdateCampaign(campaign);
                return Ok(campaign = null);
            }
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
