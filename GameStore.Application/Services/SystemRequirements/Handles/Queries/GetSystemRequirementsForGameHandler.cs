using AutoMapper;
using GameStore.Application.DTOs.SystemRequirementsDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.SystemRequirements.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.SystemRequirements.Handles.Queries
{
    public class GetSystemRequirementsForGameHandler : IRequestHandler<GetSystemRequirementsForGameRequest, IEnumerable<SystemRequirementsRetrieveDTO>>
    {
        readonly ISystemRequirementsRepository _repository;
        readonly IMapper _mapper;
        readonly IDistributedCache _cache;
        public GetSystemRequirementsForGameHandler(ISystemRequirementsRepository repository, IMapper mapper, IDistributedCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<IEnumerable<SystemRequirementsRetrieveDTO>> Handle(GetSystemRequirementsForGameRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = "GetSystemRequirementsForGame";
            var cacheData = await _cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<IEnumerable<SystemRequirementsRetrieveDTO>>(cacheData)!;
            }
            var data = await _repository.GetSystemRequirementsForGame(request.Id);
            if (data == null)
            {
                throw new NotFoundException("SystemRequirements Not Found for this game");
            }
            var map = _mapper.Map<IEnumerable<SystemRequirementsRetrieveDTO>>(data);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
            await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;
        }
    }
}
