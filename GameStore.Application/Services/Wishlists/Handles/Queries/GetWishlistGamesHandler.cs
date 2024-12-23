using AutoMapper;
using GameStore.Application.DTOs.GameDTO;
using GameStore.Application.DTOs.WishlistDTO;
using GameStore.Application.Services.Wishlists.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
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
        readonly IDistributedCache _cache;
        public GetWishlistGamesHandler(IWishlistRepository wishlistRepository, IMapper mapper, IDistributedCache cache)
        {
            _wishlistRepository = wishlistRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<IEnumerable<WishlistRetrieveDTO>> Handle(GetWishlistGamesRequest request, CancellationToken cancellationToken)
        {
            var cachekey = $"GetWishlistGames-{request.UserId}";
            var cacheData = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<IEnumerable<WishlistRetrieveDTO>>(cacheData)!;
            }
            var data = await _wishlistRepository.GetWishlistGames(request.UserId);
            var map = _mapper.Map<IEnumerable<WishlistRetrieveDTO>>(data);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;
        }
    }
}
