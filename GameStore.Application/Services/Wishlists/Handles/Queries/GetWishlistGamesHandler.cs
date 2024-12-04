using AutoMapper;
using GameStore.Application.DTOs.GameDTO;
using GameStore.Application.DTOs.WishlistDTO;
using GameStore.Application.Services.Wishlists.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Wishlists.Handles.Queries
{
    public class GetWishlistGamesHandler : IRequestHandler<GetWishlistGamesRequest, IEnumerable<WishlistRetrieveDTO>>
    {
        readonly IWishlistRepository _wishlistRepository;
        readonly IMapper _mapper;
        public GetWishlistGamesHandler(IWishlistRepository wishlistRepository, IMapper mapper)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<WishlistRetrieveDTO>> Handle(GetWishlistGamesRequest request, CancellationToken cancellationToken)
        {
            var data = await _wishlistRepository.GetWishlistGames(request.UserId);
            var map = _mapper.Map<IEnumerable<WishlistRetrieveDTO>>(data);
            return map;
        }
    }
}
