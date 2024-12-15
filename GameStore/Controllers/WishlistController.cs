using GameStore.Application.DTOs.WishlistDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Carts.Requests.Commands;
using GameStore.Application.Services.Wishlists.Requests.Commands;
using GameStore.Application.Services.Wishlists.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishlistController : ControllerBase
    {
        readonly IMediator _mediator;
        public WishlistController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("GetWishlistGames")]
        public async Task<ActionResult<IEnumerable<WishlistRetrieveDTO>>> GetWishlistGames()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var games = await _mediator.Send(new GetWishlistGamesRequest { UserId = userId });
            return Ok(games);
        }

        [Authorize]
        [HttpPost("AddGameToWishlist/{gameId}")]
        public async Task<IActionResult> AddGameToWishlist(int gameId)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var wishlist = new WishlistUploadDTO { GameId = gameId, UserId = userId };
                var id = await _mediator.Send(new AddGameToWishlistRequest { Wishlist = wishlist });
                return Ok($"Id: {id}");
            }
            catch(ValidationException ex)
            {
                return BadRequest(ex.Errors);  
            }
        }

        [Authorize]
        [HttpDelete("{gameId}")]
        public async Task<IActionResult> RemoveGameFromWishlist(int gameId)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _mediator.Send(new RemoveGameFromWishlistRequest { UserId = userId, GameId = gameId });
            return Ok();
        }
    }
}
