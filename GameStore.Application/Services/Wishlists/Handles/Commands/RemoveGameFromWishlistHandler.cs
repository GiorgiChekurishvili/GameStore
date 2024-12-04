using AutoMapper;
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
        readonly IMapper _mapper;
        public RemoveGameFromWishlistHandler(IWishlistRepository wishlistRepository, IMapper mapper, IGameRepository gameRepository)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(RemoveGameFromWishlistRequest request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<Wishlist>(request.WishlistRemove);
            await _wishlistRepository.RemoveGameFromWishlist(map);
            return Unit.Value;
        }
    }
}
