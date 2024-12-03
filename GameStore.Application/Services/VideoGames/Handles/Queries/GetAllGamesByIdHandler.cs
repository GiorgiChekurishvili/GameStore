using AutoMapper;
using GameStore.Application.DTOs.GameDTO;
using GameStore.Application.Services.VideoGames.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.VideoGames.Handles.Queries
{
    public class GetAllGamesByIdHandler : IRequestHandler<GetGameByIdRequest, GamesRetrieveDTO>
    {
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        public GetAllGamesByIdHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
        }
        public async Task<GamesRetrieveDTO> Handle(GetGameByIdRequest request, CancellationToken cancellationToken)
        {
            var data = await _gameRepository.GetGameById(request.GameId);
            var map = _mapper.Map<GamesRetrieveDTO>(data);
            return map;
        }
    }
}
