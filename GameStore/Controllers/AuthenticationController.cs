using GameStore.Application.DTOs.UserDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Authentication.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ValidationException = GameStore.Application.Exceptions.ValidationException;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserDTO loginUser)
        {
            try
            {
                var login = await _mediator.Send(new LoginRequest { UserDTO = loginUser });
                return Ok(login);
            }
            catch (NotFoundException)
            {
                return NotFound("User not found");
            }
            catch (BadRequestException)
            {
                return BadRequest("Password is incorrect");
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUser)
        {
            try
            {

                await _mediator.Send(new RegisterRequest { User = registerUser });
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }

    }
}
