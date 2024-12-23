using AutoMapper;
using GameStore.Application.DTOs.CategoryDTO;
using GameStore.Application.DTOs.GameDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Categories.Requests.Queries;
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
    public class GetAllGamesByCategoryHandler : IRequestHandler<GetAllGamesByCategoryRequest, IEnumerable<GameByCategoryRetrieveDTO>>
    {
        readonly ICategoryRepository _categoryRepository;
        readonly IMapper _mapper;
        readonly IDistributedCache _cache;
        public GetAllGamesByCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper, IDistributedCache cache)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _cache = cache;
        }
        public async Task<IEnumerable<GameByCategoryRetrieveDTO>> Handle(GetAllGamesByCategoryRequest request, CancellationToken cancellationToken)
        {
            var cachekey = $"GetAllGamesByCategory-{request.CategoryId}";
            var cacheData = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<IEnumerable<GameByCategoryRetrieveDTO>>(cacheData)!;
            }
            var data = await _categoryRepository.GetAllGamesByCategory(request.CategoryId);
            if (data == null)
            {
                throw new NotFoundException("Games Not Found");
            }
            var map = _mapper.Map<IEnumerable<GameByCategoryRetrieveDTO>>(data);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;
        }
    }
}
