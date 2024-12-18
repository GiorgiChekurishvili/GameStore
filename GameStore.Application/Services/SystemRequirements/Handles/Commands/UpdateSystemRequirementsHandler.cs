﻿using AutoMapper;
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
        readonly IMapper _mapper;
        public UpdateSystemRequirementsHandler(ISystemRequirementsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateSystemRequirementsRequest request, CancellationToken cancellationToken)
        {
            var validator = new SystemReqUploadUpdateDTOValidator();
            var validationResult = await validator.ValidateAsync(request.SysUpdateDTO!);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var data = await _repository.GetSystemRequirementsForGame(request.SysUpdateDTO!.GameId);
            if (data.Count() >= 2)
                throw new BadRequestException("there is already 2 System requirement type for this game ");
            var map = _mapper.Map<SystemRequirement>(request.SysUpdateDTO);
            map.Id = request.Id;
            await _repository.UpdateSystemRequirements(map);
            return Unit.Value;
        }
    }
}
