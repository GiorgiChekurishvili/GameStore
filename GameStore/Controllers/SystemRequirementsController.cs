using GameStore.Application.DTOs.SystemRequirementsDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.SystemRequirements.Requests.Commands;
using GameStore.Application.Services.SystemRequirements.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemRequirementsController : ControllerBase
    {
        readonly IMediator _mediator;
        public SystemRequirementsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetSystemRequirementsForGame/{id}")]
        public async Task<ActionResult<IEnumerable<SystemRequirementsRetrieveDTO>>> GetSystemRequirementsForGame(int id)
        {
            try
            {
                var data = await _mediator.Send(new GetSystemRequirementsForGameRequest { Id = id });
                return Ok(data);
            }
            catch(NotFoundException ex)
            {
                return NotFound(new {Error = ex.Message});
            }
        }


        [Authorize(Roles = "Publisher")]
        [HttpPost("AddSystemRequirements")]
        public async Task<IActionResult> AddSystemRequirements([FromBody] SystemRequirementsUploadUpdateDTO systemRequirements)
        {
            try
            {
                int publisherId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var id = await _mediator.Send(new AddSystemRequirementsRequest { SysUploadDTO = systemRequirements, PublisherId = publisherId });
                return Ok($"System Requirements Id: {id}");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPut("UpdateSystemRequirements/{id}")]
        public async Task<IActionResult> UpdateSystemRequirements(int id, [FromBody] SystemRequirementsUploadUpdateDTO systemRequirements)
        {
            try
            {
                int publisherId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                await _mediator.Send(new UpdateSystemRequirementsRequest { Id = id, SysUpdateDTO = systemRequirements, PublisherId = publisherId });
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }

        }

    }
}
