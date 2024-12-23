using GameStore.Application.Cache;
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
        readonly ICacheService _cacheService; 
        public RemoveAllGamesFromCartHandler(ICartRepository cartRepository, ICacheService cacheService)
        {
            _cartRepository = cartRepository;
            _cacheService = cacheService;
        }

        public async Task<Unit> Handle(RemoveAllGamesFromCartRequest request, CancellationToken cancellationToken)
        {
            await _cartRepository.RemoveAllGamesFromCart(request.UserId);
            await _cacheService.RemoveCache("GetCartGames", request.UserId);
            return Unit.Value;
        }
    }
}
