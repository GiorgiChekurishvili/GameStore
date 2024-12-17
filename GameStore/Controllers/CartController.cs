using GameStore.Application.DTOs.CartDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Carts.Requests.Commands;
using GameStore.Application.Services.Carts.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        readonly IMediator _mediator;
        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("GetCartGames")]
        public async Task<ActionResult<IEnumerable<CartRetrieveDTO>>> GetCartGames()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var data = await _mediator.Send(new GetCartGamesRequest { UserId = userId });
            return Ok(data);

        }
        [Authorize(Roles = "Customer")]
        [HttpPost("AddGameToCart")]
        public async Task<IActionResult> AddGameToCart(int gameId)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                CartCommandsDTO cartDTO = new CartCommandsDTO() { GameId = gameId, UserId = userId };
                await _mediator.Send(new AddGameToCartRequest { CartDTO = cartDTO });
                return Ok(cartDTO);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }


        }
        [Authorize(Roles = "Customer")]
        [HttpPost("CheckoutGames")]
        public async Task<IActionResult> CheckoutGames()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _mediator.Send(new CheckoutGamesRequest { UserId = userId });
            return Ok();

        }
        [Authorize(Roles = "Customer")]
        [HttpDelete("RemoveAllGamesFromCart")]
        public async Task<IActionResult> RemoveAllGamesFromCart()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            await _mediator.Send(new RemoveAllGamesFromCartRequest { UserId = userId });
            return Ok();
        }
        [Authorize(Roles = "Customer")]
        [HttpDelete("RemoveGameFromCartRequest")]
        public async Task<IActionResult> RemoveGameFromCartRequest(int gameId)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                CartCommandsDTO cartDTO = new CartCommandsDTO() { GameId = gameId, UserId = userId };
                await _mediator.Send(new RemoveGameFromCartRequest { CartDTO = cartDTO });
                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }
    }
}
