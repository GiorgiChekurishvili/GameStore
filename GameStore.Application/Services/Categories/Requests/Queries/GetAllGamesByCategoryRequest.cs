using GameStore.Application.DTOs.CategoryDTO;
using GameStore.Application.DTOs.GameDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Categories.Requests.Queries
{
    public class GetAllGamesByCategoryRequest : IRequest<IEnumerable<GameByCategoryRetrieveDTO>>
    {
        public int CategoryId { get; set; }
    }
}
