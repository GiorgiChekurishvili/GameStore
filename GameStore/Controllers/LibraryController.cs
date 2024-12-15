using GameStore.Application.DTOs.LibraryDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Library.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;


namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        readonly IMediator _mediator;
        public LibraryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("GetAllLibraryGames")]
        public async Task<ActionResult<IEnumerable<LibraryRetrieveDTO>>> GetAllLibraryGames()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var games = await _mediator.Send(new GetAllLibraryGamesRequest { UserId = userId});
            return Ok(games);
        }
        [Authorize]
        [HttpGet("GetLibraryGameById/{id}")]
        public async Task<ActionResult<LibraryRetrieveDTO>> GetLibraryGameById(int gameid)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var game = await _mediator.Send(new GetLibraryGameByIdRequest { GameId = gameid, UserId = userId});
                return Ok(game);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(new {Error = ex.Message});
            }
        }
    }
}
