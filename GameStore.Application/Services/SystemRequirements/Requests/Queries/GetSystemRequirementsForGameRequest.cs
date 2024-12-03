using GameStore.Application.DTOs.SystemRequirementsDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.SystemRequirements.Requests.Queries
{
    public class GetSystemRequirementsForGameRequest : IRequest<IEnumerable<SystemRequirementsRetrieveDTO>>
    {
        public int Id { get; set; }
    }
}
