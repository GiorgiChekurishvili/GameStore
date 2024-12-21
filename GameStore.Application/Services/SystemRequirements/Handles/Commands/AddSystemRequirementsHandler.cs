using AutoMapper;
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
    public class AddSystemRequirementsHandler : IRequestHandler<AddSystemRequirementsRequest, int>
    {
        readonly ISystemRequirementsRepository _repository;
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        public AddSystemRequirementsHandler(ISystemRequirementsRepository repository, IMapper mapper, IGameRepository gameRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _gameRepository = gameRepository;
        }

        public async Task<int> Handle(AddSystemRequirementsRequest request, CancellationToken cancellationToken)
        {
            var validator = new SystemReqUploadUpdateDTOValidator();
            var validationResult = await validator.ValidateAsync(request.SysUploadDTO!);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var data = await _repository.GetSystemRequirementsForGame(request.SysUploadDTO!.GameId);
            var osCounts = data
                .GroupBy(d => d.Os)
                .ToDictionary(g => g.Key, g => g.Count());

            string newOs = request.SysUploadDTO.Os!;

            if (osCounts.TryGetValue(newOs, out int currentCount) && currentCount >= 2)
            {
                throw new BadRequestException($"There are already 2 entries for the OS '{newOs}'.");
            }

            var gameDetails = await _gameRepository.GetGameById(request.SysUploadDTO!.GameId);
            if (gameDetails.PublisherId != request.PublisherId)
            {
                throw new BadRequestException("You are not allowed to update system requirements for this game");
            }
            var map = _mapper.Map<SystemRequirement>(request.SysUploadDTO);
            var id = await _repository.AddSystemRequirements(map);
            return id;
        }
    }
}
