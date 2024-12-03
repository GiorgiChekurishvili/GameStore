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
    public class GetLibraryGameByIdHandler : IRequestHandler<GetLibraryGameByIdRequest, LibraryRetrieveDTO>
    {
        readonly ILibraryRepository _libraryRepository;
        readonly IMapper _mapper;
        public GetLibraryGameByIdHandler(ILibraryRepository libraryRepository, IMapper mapper)
        {
            _libraryRepository = libraryRepository;
            _mapper = mapper;
        }
        public async Task<LibraryRetrieveDTO> Handle(GetLibraryGameByIdRequest request, CancellationToken cancellationToken)
        {
            var data = await _libraryRepository.GetLibraryGameById(request.Id);
            if (data != null)
            {
                var map = _mapper.Map<LibraryRetrieveDTO>(data);
                return map;
            }
            return null;

        }
    }
}
