using GameStore.Application.DTOs.SystemRequirementsDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.SystemRequirements.Requests.Commands
{
    public class UpdateSystemRequirementsRequest : IRequest<Unit>
    {
        public int Id { get; set; }
        public SystemRequirementsUploadUpdateDTO? SysUpdateDTO { get; set; }
        public int PublisherId { get; set; }
    }
}
