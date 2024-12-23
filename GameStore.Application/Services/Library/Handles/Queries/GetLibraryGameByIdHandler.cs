using AutoMapper;
using GameStore.Application.DTOs.LibraryDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Library.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace GameStore.Application.Services.Library.Handles.Queries
{
    public class GetLibraryGameByIdHandler : IRequestHandler<GetLibraryGameByIdRequest, LibraryRetrieveDTO>
    {
        readonly ILibraryRepository _libraryRepository;
        readonly IMapper _mapper;
        readonly IDistributedCache _cache;
        public GetLibraryGameByIdHandler(ILibraryRepository libraryRepository, IMapper mapper, IDistributedCache cache)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<LibraryRetrieveDTO> Handle(GetLibraryGameByIdRequest request, CancellationToken cancellationToken)
        {
            var cachekey = $"GetLibraryGameById-{request.GameId}";
            var cacheData = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<LibraryRetrieveDTO>(cacheData)!;
            }
            var data = await _libraryRepository.GetLibraryGameById(request.GameId, request.UserId);
            if (data == null)
            {
                throw new NotFoundException("This game doesnt exist in your library ");
            }
            var map = _mapper.Map<LibraryRetrieveDTO>(data);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(2))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));
            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;

        }
    }
}
