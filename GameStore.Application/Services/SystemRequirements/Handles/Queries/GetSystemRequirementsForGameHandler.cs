using AutoMapper;
using GameStore.Application.DTOs.SystemRequirementsDTO;
using GameStore.Application.Services.SystemRequirements.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.SystemRequirements.Handles.Queries
{
    public class GetSystemRequirementsForGameHandler : IRequestHandler<GetSystemRequirementsForGameRequest, IEnumerable<SystemRequirementsRetrieveDTO>>
    {
        readonly ISystemRequirementsRepository _repository;
        readonly IMapper _mapper;
        public GetSystemRequirementsForGameHandler(ISystemRequirementsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SystemRequirementsRetrieveDTO>> Handle(GetSystemRequirementsForGameRequest request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetSystemRequirementsForGame(request.Id);
            if (data != null)
            {
                var map = _mapper.Map<IEnumerable<SystemRequirementsRetrieveDTO>>(data);
                return map;
            }
            return null;

        }
    }
}
