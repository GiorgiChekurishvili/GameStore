using AutoMapper;
using GameStore.Application.DTOs.CategoryDTO;
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
    public class GetAllGamesByCategoryHandler : IRequestHandler<GetAllGamesByCategoryRequest, IEnumerable<GamesRetrieveDTO>>
    {
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        readonly IDistributedCache _cache;
        public GetAllGamesByCategoryHandler(IGameRepository gameRepository, IMapper mapper, IDistributedCache cache)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<GamesRetrieveDTO>> Handle(GetAllGamesByCategoryRequest request, CancellationToken cancellationToken)
        {
            var cachekey = $"GetAllGamesByCategory-{request.CategoryId}";
            var cacheData = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<IEnumerable<GamesRetrieveDTO>>(cacheData)!;
            }
            var data = await _gameRepository.GetAllGamesByCategory(request.CategoryId);
            if (data == null)
            {
                throw new NotFoundException("Games Not Found");
            }
            var map = _mapper.Map<IEnumerable<GamesRetrieveDTO>>(data);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;
        }
    }
}
