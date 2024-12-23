using AutoMapper;
using GameStore.Application.Cache;
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
    public class AddGameHandler : IRequestHandler<AddGameRequest, int>
    {
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        readonly ICacheService _cacheService;
        public AddGameHandler(IGameRepository gameRepository, IMapper mapper, ICacheService cacheService)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
            _cacheService = cacheService;
        }
        public async Task<int> Handle(AddGameRequest request, CancellationToken cancellationToken)
        {
            var validator = new GameUploadUpdateDTOValidator();
            var validationResult = await validator.ValidateAsync(request.GameUploadDTO!);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var datas = await _gameRepository.GetAllGames();
            foreach (var data in datas)
            {
                if (data.Name.ToLower() == request.GameUploadDTO.Name!.ToLower())
                {
                    throw new BadRequestException("Game Already Exists");
                }
            }
            var game = _mapper.Map<Game>(request.GameUploadDTO);
            game.PublisherId = request.PublisherId;
            var id = await _gameRepository.AddGame(game);
            await _cacheService.RemoveCache("GetAllGames");
            await _cacheService.RemoveCache("GetGameById", id);
            await _cacheService.RemoveCache("GetAllGamesByPublisher", request.PublisherId);
            return id;
        }
    }
}
