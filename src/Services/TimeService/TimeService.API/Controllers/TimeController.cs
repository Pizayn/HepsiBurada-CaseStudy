using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using TimeService.API.Entities;
using TimeService.API.Repositories;

namespace TimeService.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TimeController : ControllerBase
    {

        private readonly ITimeRepository _timeRepository;

        public TimeController(ITimeRepository timeRepository)
        {
            _timeRepository = timeRepository ?? throw new ArgumentNullException(nameof(timeRepository)); 
        }


        [HttpGet("[action]/{hour}", Name = "IncreaseTime")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Time), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Time>> IncreaseTime(int hour)
        {
            var existTime = await _timeRepository.GetTime();
            existTime.Hour = hour;
            await _timeRepository.UpdateTime(existTime);

            return Ok();
        }

        [HttpGet( Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Time), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Time>> GetTime()
        {
            var time = await _timeRepository.GetTime();
            return Ok(time);
        }
    }
}
