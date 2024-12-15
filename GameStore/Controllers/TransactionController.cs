using GameStore.Application.DTOs.TransactionDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Transactions.Requests.Commands;
using GameStore.Application.Services.Transactions.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        readonly IMediator _mediator;
        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpGet("GetAllTransactionsByUserId")]
        public async Task<ActionResult<IEnumerable<TransactionRetrieveDTO>>> GetAllTransactionsByUserId()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var transactions = await _mediator.Send(new GetAllTransactionsByUserIdRequest { UserId = userId });
            return Ok(transactions);

        }

        [Authorize]
        [HttpGet("GetUserBalance")]
        public async Task<IActionResult> GetUserBalance()
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var balance = await _mediator.Send(new GetUserBalanceRequest { UserId = userId });
            return Ok("Balance: " + balance);
        }

        [Authorize]
        [HttpPost("FillBalance")]
        public async Task<IActionResult> FillUserBalance(decimal balance)
        {
            try
            {
                var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var transaction = new FIllBalanceTransactionDTO { Balance = balance, UserId = userId };
                var newBalance = await _mediator.Send(new FillBalanceByUserIdRequest { FillBalance = transaction });
                return Ok(newBalance);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors);
            }
        }
    }
}
