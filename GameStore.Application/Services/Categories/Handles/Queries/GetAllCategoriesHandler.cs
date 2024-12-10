using AutoMapper;
using GameStore.Application.DTOs.CategoryDTO;
using GameStore.Application.Services.Categories.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Categories.Handles.Queries
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesRequest, CategoryUpdateDTO>
    {
        readonly ICategoryRepository _categoryRepository;
        readonly IMapper _mapper;
        
        public GetAllCategoriesHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryUpdateDTO> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            var data = await _categoryRepository.GetAllCategories();
            var map = _mapper.Map<CategoryUpdateDTO>(data);
            return map;
        }
    }
}
