using GameStore.Application.DTOs.GameDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.VideoGames.Requests.Commands
{
    public class UpdateGameRequest : IRequest<Unit>
    {
        public int Id { get; set; }
        public required GameUploadUpdateDTO GameUpdateDTO { get; set; }
    }
}
