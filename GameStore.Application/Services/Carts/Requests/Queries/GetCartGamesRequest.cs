using GameStore.Application.DTOs.CartDTO;
using GameStore.Application.DTOs.GameDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Carts.Requests.Queries
{
    public class GetCartGamesRequest : IRequest<IEnumerable<CartRetrieveDTO>>
    {
        public int UserId { get; set; }
    }
}
