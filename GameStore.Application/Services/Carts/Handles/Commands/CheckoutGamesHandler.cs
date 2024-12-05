using AutoMapper;
using GameStore.Application.Services.Carts.Requests.Commands;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Carts.Handles.Commands
{
    public class CheckoutGamesHandler : IRequestHandler<CheckoutGamesRequest, Unit>
    {
        readonly ICartRepository _cartRepository;
        public CheckoutGamesHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<Unit> Handle(CheckoutGamesRequest request, CancellationToken cancellationToken)
        {
            await _cartRepository.CheckoutGames(request.UserId);
            return Unit.Value;
        }
    }
}
