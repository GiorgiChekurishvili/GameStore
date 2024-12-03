using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.VideoGames.Requests.Commands
{
    public class DeleteGameRequest : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
