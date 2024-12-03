using AutoMapper;
using GameStore.Application.Services.VideoGames.Requests.Commands;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.VideoGames.Handles.Commands
{
    public class DeleteGameHandler : IRequestHandler<DeleteGameRequest, Unit>
    {
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        public DeleteGameHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
        }
        public async Task<Unit> Handle(DeleteGameRequest request, CancellationToken cancellationToken)
        {
            await _gameRepository.DeleteGame(request.Id);
            return Unit.Value;
        }
    }
}
