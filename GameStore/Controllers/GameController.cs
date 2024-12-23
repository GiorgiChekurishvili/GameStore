using GameStore.Application.DTOs.GameDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.VideoGames.Requests.Commands;
using GameStore.Application.Services.VideoGames.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        readonly IMediator _mediator;
        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAllGames")]
        public async Task<ActionResult<IEnumerable<GamesRetrieveDTO>>> GetAllGames()
        {
            var games = await _mediator.Send(new GetAllGamesRequest());
            return Ok(games);
        }

        [HttpGet("GetAllGamesByPublisher")]
        public async Task<ActionResult<IEnumerable<GamesRetrieveDTO>>> GetAllGamesByPublisher(int publisherId)
        {
            var games = await _mediator.Send(new GetAllGamesByPublisherIdRequest { UserId = publisherId});
            return Ok(games);
        }
        [HttpGet("GetGameById/{id}")]
        public async Task<ActionResult<GamesRetrieveDTO>> GetGameById(int id)
        {
            var game = await _mediator.Send(new GetGameByIdRequest { GameId = id });
            return Ok(game);
        }

        [Authorize(Roles = "Publisher")]
        [HttpPost("AddGame")]
        public async Task<IActionResult> AddGame([FromBody] GameUploadUpdateDTO gameUpload)
        {
            try
            {
                var publisherId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var game = await _mediator.Send(new AddGameRequest { GameUploadDTO = gameUpload, PublisherId = publisherId });
                return Ok("GameId: " + game);
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

        [Authorize(Roles = "Publisher")]
        [HttpPut("UpdateGame/{id}")]
        public async Task<IActionResult> UpdateGame(int id, [FromBody] GameUploadUpdateDTO updateDTO)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                await _mediator.Send(new UpdateGameRequest { Id = id, GameUpdateDTO = updateDTO, PublisherId = userId });
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(new {Error = ex.Message});
            }
        }

        [Authorize(Roles = "Publisher")]
        [HttpDelete("DeleteGame/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await _mediator.Send(new DeleteGameRequest { Id = id });
            return Ok();
        }
    }
}
