using AutoMapper;
using GameStore.Application.Services.VideoGames.Requests.Commands;
using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.VideoGames.Handles.Commands
{
    public class UpdateGameHandler : IRequestHandler<UpdateGameRequest, Unit>
    {
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        public UpdateGameHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
        }

        public async Task<Unit> Handle(UpdateGameRequest request, CancellationToken cancellationToken)
        {
            var data = await _gameRepository.GetGameById(request.Id);
            if ( data == null )
            {
                return Unit.Value;
            }
            var map = _mapper.Map<Game>(request.GameUpdateDTO);
            await _gameRepository.UpdateGame(map);
            return Unit.Value;
        }
    }
}
