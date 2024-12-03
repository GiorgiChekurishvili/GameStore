using GameStore.Application.DTOs.GameDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.VideoGames.Requests.Commands
{
    public class AddGameRequest : IRequest<int>
    {
        public required GameUploadUpdateDTO GameUploadDTO { get; set; }
    }
}
