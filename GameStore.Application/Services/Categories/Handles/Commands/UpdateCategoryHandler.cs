using AutoMapper;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Categories.Requests.Commands;
using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Categories.Handles.Commands
{
    public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryRequest, Unit>
    {
        readonly ICategoryRepository _categoryRepository;
        readonly IMapper _mapper;

        public UpdateCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllCategories();
            foreach (var category in categories)
            {
                if (category.CategoryName.ToLower() == request.Category.CategoryName.ToLower())
                {
                    throw new BadRequestException($"A Category named '{category.CategoryName}' already exists.");
                }
            }
            var map = _mapper.Map<Category>(request.Category);
            await _categoryRepository.UpdateCategory(map);
            return Unit.Value;
        }
    }
}
