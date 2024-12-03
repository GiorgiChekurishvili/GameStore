using AutoMapper;
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
        readonly IMapper _mapper;
        public AddSystemRequirementsHandler(ISystemRequirementsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddSystemRequirementsRequest request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<SystemRequirement>(request.SysUploadDTO);
            var id = await _repository.AddSystemRequirements(map);
            return id;
        }
    }
}
