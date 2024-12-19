using AutoMapper;
using GameStore.Application.DTOs.GameDTO.Validators;
using GameStore.Application.Exceptions;
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
        readonly IGameCategoryRepository _gameCategoryRepository;
        readonly IMapper _mapper;
        public UpdateGameHandler(IGameRepository gameRepository, IMapper mapper, IGameCategoryRepository gameCategoryRepository)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
            _gameCategoryRepository = gameCategoryRepository;
        }

        public async Task<Unit> Handle(UpdateGameRequest request, CancellationToken cancellationToken)
        {
            var validator = new GameUploadUpdateDTOValidator();
            var validationResult = await validator.ValidateAsync(request.GameUpdateDTO!);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var gameId = await _gameRepository.GetGameById(request.Id);
            if ( gameId == null )
            {
                throw new NotFoundException("Game not found()");
            }
            var map = _mapper.Map<Game>(request.GameUpdateDTO);
            map.Id = request.Id;
            map.PublisherId = request.PublisherId;
            await _gameRepository.UpdateGame(map);
            await _gameCategoryRepository.UpdateGameCategory(request.GameUpdateDTO.CategoryIds!, request.Id);
            return Unit.Value;
        }
    }
}
