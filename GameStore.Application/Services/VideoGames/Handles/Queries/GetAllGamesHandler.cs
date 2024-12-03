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
    public class GetAllGamesHandler : IRequestHandler<GetAllGamesRequest, IEnumerable<GamesRetrieveDTO>>
    {
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        public GetAllGamesHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
        }
        public async Task<IEnumerable<GamesRetrieveDTO>> Handle(GetAllGamesRequest request, CancellationToken cancellationToken)
        {
            var games = await _gameRepository.GetAllGames();
            var map = _mapper.Map<IEnumerable<GamesRetrieveDTO>>(games);
            return map;
        }
    }
}
