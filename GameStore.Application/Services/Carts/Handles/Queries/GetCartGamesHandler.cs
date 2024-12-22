using AutoMapper;
using GameStore.Application.DTOs.CartDTO;
using GameStore.Application.Services.Carts.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Carts.Handles.Queries
{
    public class GetCartGamesHandler : IRequestHandler<GetCartGamesRequest, IEnumerable<CartRetrieveDTO>>
    {
        readonly ICartRepository _cartRepository;
        readonly IMapper _mapper;
        readonly IDistributedCache _cache;
        public GetCartGamesHandler(ICartRepository cartRepository, IMapper mapper, IDistributedCache cache)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<IEnumerable<CartRetrieveDTO>> Handle(GetCartGamesRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = "GetCartGames";
            var cacheData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<IEnumerable<CartRetrieveDTO>>(cacheData)!;
            }
            var data = await _cartRepository.GetCartGames(request.UserId);
            var map = _mapper.Map<IEnumerable<CartRetrieveDTO>>(data);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
            await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;
        }
    }
}
