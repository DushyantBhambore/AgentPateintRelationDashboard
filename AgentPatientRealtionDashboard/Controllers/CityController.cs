using App.Core.Apps.City.Command;
using App.Core.Apps.City.Query;
using App.Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentPatientRealtionDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddCity")]
        public async Task<IActionResult> AddCity([FromBody] CityDto city)
        {
            var result = await _mediator.Send(new AddCityCommand { city = city });
            return Ok(result);
        }

        [HttpGet("GetAllCity")]
        public async Task<IActionResult> GetAllCity()
        {
            var result = await _mediator.Send(new GetAllCityQuery());
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCityById(int Id)
        {
            var result = await _mediator.Send(new GetCityByIdQuery { Id = Id });
            return Ok(result);
        }
    }
}
