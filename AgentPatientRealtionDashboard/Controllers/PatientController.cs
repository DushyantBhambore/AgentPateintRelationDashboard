using App.Core.Apps.Patient.Command;
using App.Core.Apps.Patient.Query;
using App.Core.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AgentPatientRealtionDashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("AddPatient")]
        public async Task<IActionResult> AddPatient([FromBody] PatientDto patientDto)
        {
            var result = await _mediator.Send(new AddPatientCommand { patient = patientDto });
            return Ok(result);
        }

        [HttpGet("GetAllPatient")]
        public async Task<IActionResult> GetAllPatient()
        {
            var result = await _mediator.Send(new GetAllPatientQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {
            var result = await _mediator.Send(new GetPatientByIdQuery { id = id });
            return Ok(result);
        }

        [HttpPut("UpdatePatient")]
        public async Task<IActionResult> UpdatePatient([FromBody] PatientDto patientDto)
        {
            var result = await _mediator.Send(new UpdatePatientCommand { patient = patientDto });
            return Ok(result);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var result = await _mediator.Send(new DeletePatientCommand { Id = id });
            return Ok(result);
        }
    }
}
