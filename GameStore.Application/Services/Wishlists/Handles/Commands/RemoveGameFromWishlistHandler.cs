using AutoMapper;
using GameStore.Application.Cache;
using GameStore.Application.Services.Wishlists.Requests.Commands;
using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Wishlists.Handles.Commands
{
    public class RemoveGameFromWishlistHandler : IRequestHandler<RemoveGameFromWishlistRequest, Unit>
    {
        readonly IWishlistRepository _wishlistRepository;
        readonly ICacheService _cacheService;
        public RemoveGameFromWishlistHandler(IWishlistRepository wishlistRepository, IGameRepository gameRepository, ICacheService cacheService)
        {
            _wishlistRepository = wishlistRepository;
            _cacheService = cacheService;
        }
        public async Task<Unit> Handle(RemoveGameFromWishlistRequest request, CancellationToken cancellationToken)
        {
            var wishlist = new Wishlist { GameId = request.GameId, UserId = request.UserId };
            await _wishlistRepository.RemoveGameFromWishlist(wishlist);
            await _cacheService.RemoveCache("GetWishlistGames", request.UserId);
            return Unit.Value;
        }
    }
}
