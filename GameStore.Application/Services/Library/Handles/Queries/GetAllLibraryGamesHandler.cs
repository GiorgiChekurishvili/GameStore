using AutoMapper;
using GameStore.Application.DTOs.LibraryDTO;
using GameStore.Application.Services.Library.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
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
        public GetAllLibraryGamesHandler(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LibraryRetrieveDTO>> Handle(GetAllLibraryGamesRequest request, CancellationToken cancellationToken)
        {
            var data = await _libraryRepository.GeAllLibraryGames(request.UserId);
            var map = _mapper.Map<IEnumerable<LibraryRetrieveDTO>>(data);
            return map;
        }
    }
}
