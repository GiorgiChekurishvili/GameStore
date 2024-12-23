using AutoMapper;
using GameStore.Application.Cache;
using GameStore.Application.DTOs.CategoryDTO;
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
    public class AddCategoryHandler : IRequestHandler<AddCategoryRequest, int>
    {
        readonly ICategoryRepository _categoryRepository;
        readonly ICacheService _cacheService;

        public AddCategoryHandler(ICategoryRepository categoryRepository, ICacheService cacheService)
        {
            _categoryRepository = categoryRepository;
            _cacheService = cacheService;
        }
        public async Task<int> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryRepository.GetAllCategories();
            foreach (var category in categories)
            {
                if (category.CategoryName.ToLower() == request.CategoryName.ToLower())
                {
                    throw new BadRequestException($"A Category named '{category.CategoryName}' already exists.");
                }
            }
            Category newCategory = new Category { CategoryName = request.CategoryName };
            var Categoryid = await _categoryRepository.AddCategory(newCategory);
            await _cacheService.RemoveCache("GetAllCategories");
            await _cacheService.RemoveCache("GetAllGamesByCategory", Categoryid);
            return Categoryid;
        }
    }
}
