using GameStore.Application.DTOs.UserDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Authentication.Requests.Commands
{
    public class LoginRequest : IRequest<string>
    {
        public LoginUserDTO? UserDTO { get; set; }
    }
}
