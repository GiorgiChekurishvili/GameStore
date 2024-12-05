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
    public class RemoveAllGamesFromCartHandler : IRequestHandler<RemoveAllGamesFromCartRequest, Unit>
    {
        readonly ICartRepository _cartRepository;
        public RemoveAllGamesFromCartHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Unit> Handle(RemoveAllGamesFromCartRequest request, CancellationToken cancellationToken)
        {
            await _cartRepository.RemoveAllGamesFromCart(request.UserId);
            return Unit.Value;
        }
    }
}
