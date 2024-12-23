using AutoMapper;
using GameStore.Application.DTOs.GameDTO;
using GameStore.Application.Exceptions;
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
    public class GetGameByIdHandler : IRequestHandler<GetGameByIdRequest, GamesRetrieveDTO>
    {
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        readonly IDistributedCache _cache;
        public GetGameByIdHandler(IGameRepository gameRepository, IMapper mapper, IDistributedCache cache)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
            _cache = cache;
        }
        public async Task<GamesRetrieveDTO> Handle(GetGameByIdRequest request, CancellationToken cancellationToken)
        {
            var cachekey = $"GetGameById-{request.GameId}";
            var cacheData = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<GamesRetrieveDTO>(cacheData)!;
            }
            var data = await _gameRepository.GetGameById(request.GameId);
            if (data == null)
            {
                throw new NotFoundException("Game Not Found");
            }
            var map = _mapper.Map<GamesRetrieveDTO>(data);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;
        }
    }
}
