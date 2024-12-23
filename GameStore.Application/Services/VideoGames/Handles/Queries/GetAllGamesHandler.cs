using AutoMapper;
using GameStore.Application.DTOs.GameDTO;
using GameStore.Application.Services.VideoGames.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.VideoGames.Handles.Queries
{
    public class GetAllGamesHandler : IRequestHandler<GetAllGamesRequest, IEnumerable<GamesRetrieveDTO>>
    {
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        readonly IDistributedCache _cache;
        public GetAllGamesHandler(IGameRepository gameRepository, IMapper mapper, IDistributedCache cache)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<GamesRetrieveDTO>> Handle(GetAllGamesRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = "GetAllGames";
            var cacheData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<IEnumerable<GamesRetrieveDTO>>(cacheData)!;
            }
            var games = await _gameRepository.GetAllGames();
            var map = _mapper.Map<IEnumerable<GamesRetrieveDTO>>(games);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(10))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));
            await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;
        }
    }
}
