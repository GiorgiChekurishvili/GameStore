using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Carts.Requests.Commands
{
    public class CheckoutGamesRequest : IRequest<Unit>
    {
        public int UserId { get; set; }
    }
}
