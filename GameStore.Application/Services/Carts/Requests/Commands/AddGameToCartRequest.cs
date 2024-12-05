using GameStore.Application.DTOs.CartDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Carts.Requests.Commands
{
    public class AddGameToCartRequest : IRequest<Unit>
    {
        public CartCommandsDTO? CartDTO { get; set; }
    }
}
