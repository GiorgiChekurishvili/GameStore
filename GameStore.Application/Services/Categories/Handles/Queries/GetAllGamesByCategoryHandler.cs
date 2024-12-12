using AutoMapper;
using GameStore.Application.DTOs.CategoryDTO;
using GameStore.Application.DTOs.GameDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Categories.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.VideoGames.Handles.Queries
{
    public class GetAllGamesByCategoryHandler : IRequestHandler<GetAllGamesByCategoryRequest, IEnumerable<GameByCategoryRetrieveDTO>>
    {
        readonly ICategoryRepository _categoryRepository;
        readonly IMapper _mapper;
        public GetAllGamesByCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<GameByCategoryRetrieveDTO>> Handle(GetAllGamesByCategoryRequest request, CancellationToken cancellationToken)
        {

            var data = await _categoryRepository.GetAllGamesByCategory(request.CategoryId);
            if (data == null)
            {
                throw new NotFoundException("Games Not Found");
            }
            var map = _mapper.Map<IEnumerable<GameByCategoryRetrieveDTO>>(data);
            return map;
        }
    }
}
