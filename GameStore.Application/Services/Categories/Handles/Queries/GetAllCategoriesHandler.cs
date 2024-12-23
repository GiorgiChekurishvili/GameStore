using AutoMapper;
using GameStore.Application.DTOs.CategoryDTO;
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

namespace GameStore.Application.Services.Categories.Handles.Queries
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesRequest, IEnumerable<CategoriesRetrieveDTO>>
    {
        readonly ICategoryRepository _categoryRepository;
        readonly IMapper _mapper;
        readonly IDistributedCache _cache;
        
        public GetAllCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper, IDistributedCache cache)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<IEnumerable<CategoriesRetrieveDTO>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            var cachekey = "GetAllCategories";
            var cacheData = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<IEnumerable<CategoriesRetrieveDTO>>(cacheData)!;
            }
            var data = await _categoryRepository.GetAllCategories();
            var map = _mapper.Map<IEnumerable<CategoriesRetrieveDTO>>(data);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30))
                .SetAbsoluteExpiration(TimeSpan.FromHours(5));
            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;
        }
    }
}
