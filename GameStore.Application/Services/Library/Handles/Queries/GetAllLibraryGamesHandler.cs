using AutoMapper;
using GameStore.Application.DTOs.LibraryDTO;
using GameStore.Application.Services.Library.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Library.Handles.Queries
{
    public class GetAllLibraryGamesHandler : IRequestHandler<GetAllLibraryGamesRequest, IEnumerable<LibraryRetrieveDTO>>
    {
        readonly ILibraryRepository _libraryRepository;
        readonly IMapper _mapper;
        readonly IDistributedCache _cache;
        public GetAllLibraryGamesHandler(ILibraryRepository libraryRepository, IMapper mapper, IDistributedCache cache)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<IEnumerable<LibraryRetrieveDTO>> Handle(GetAllLibraryGamesRequest request, CancellationToken cancellationToken)
        {
            var cachekey = $"GetAllLibraryGames-{request.UserId}";
            var cacheData = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<IEnumerable<LibraryRetrieveDTO>>(cacheData)!;
            }
            var data = await _libraryRepository.GeAllLibraryGames(request.UserId);
            var map = _mapper.Map<IEnumerable<LibraryRetrieveDTO>>(data);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;
        }
    }
}
