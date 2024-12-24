using AutoMapper;
using GameStore.Application.Cache;
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
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryRequest, Unit>
    {
        readonly ICategoryRepository _categoryRepository;
        readonly ICacheService _cacheService;

        public DeleteCategoryHandler(ICategoryRepository categoryRepository, ICacheService cacheService)
        {
            _categoryRepository = categoryRepository;
            _cacheService = cacheService;
        }

        public async Task<Unit> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            await _categoryRepository.DeleteCategory(request.Id);
            await _cacheService.RemoveCache("GetAllCategories");
            await _cacheService.RemoveCache("GetAllGamesByCategory", request.Id);
            await _cacheService.RemoveCache("GetCategoryById", request.Id);
            return Unit.Value;
            
        }
    }
}
