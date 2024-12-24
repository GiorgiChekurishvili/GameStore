using AutoMapper;
using GameStore.Application.DTOs.CategoryDTO;
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

namespace GameStore.Application.Services.Categories.Handles.Queries
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdRequest, CategoriesRetrieveDTO>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetCategoryByIdHandler(ICategoryRepository categoryRepository, IMapper mapper, IDistributedCache cache)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<CategoriesRetrieveDTO> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"GetCategoryById-{request.Id}";
            var cacheData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<CategoriesRetrieveDTO>(cacheData)!;
            }
            var data = await _categoryRepository.GetCategoryById(request.Id);
            if (data == null)
            {
                throw new NotFoundException("Category Not Found");
            }
            var map = _mapper.Map<CategoriesRetrieveDTO>(data);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
            await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;
        }
    }
}
