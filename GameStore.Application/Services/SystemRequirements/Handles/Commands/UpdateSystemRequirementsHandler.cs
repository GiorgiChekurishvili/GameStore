using AutoMapper;
using GameStore.Application.Cache;
using GameStore.Application.DTOs.SystemRequirementsDTO.Validators;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.SystemRequirements.Requests.Commands;
using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.SystemRequirements.Handles.Commands
{
    public class UpdateSystemRequirementsHandler : IRequestHandler<UpdateSystemRequirementsRequest, Unit>
    {
        readonly ISystemRequirementsRepository _repository;
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        readonly ICacheService _cacheService;
        public UpdateSystemRequirementsHandler(ISystemRequirementsRepository repository, IMapper mapper, IGameRepository gameRepository, ICacheService cacheService)
        {
            _repository = repository;
            _mapper = mapper;
            _gameRepository = gameRepository;
            _cacheService = cacheService;
        }
        public async Task<Unit> Handle(UpdateSystemRequirementsRequest request, CancellationToken cancellationToken)
        {
            var validator = new SystemReqUploadUpdateDTOValidator();
            var validationResult = await validator.ValidateAsync(request.SysUpdateDTO!);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var gameDetails = await _gameRepository.GetGameById(request.SysUpdateDTO!.GameId);

            if (gameDetails.PublisherId != request.PublisherId)
            {
                throw new BadRequestException("You are not allowed to update system requirements for this game");
            }
            var map = _mapper.Map<SystemRequirement>(request.SysUpdateDTO);
            map.Id = request.Id;
            await _repository.UpdateSystemRequirements(map);
            await _cacheService.RemoveCache("GetSystemRequirementsForGame", request.SysUpdateDTO.GameId);
            return Unit.Value;
        }
    }
}
