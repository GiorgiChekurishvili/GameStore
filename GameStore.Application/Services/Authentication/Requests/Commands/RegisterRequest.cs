using GameStore.Application.DTOs.UserDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Authentication.Requests.Commands
{
    public class RegisterRequest : IRequest<Unit>
    {
        public RegisterUserDTO? User { get; set; }
    }
}
