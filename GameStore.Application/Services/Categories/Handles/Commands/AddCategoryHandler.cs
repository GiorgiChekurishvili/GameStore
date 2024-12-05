using GameStore.Application.Services.Categories.Requests.Commands;
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

        public AddCategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Task<int> Handle(AddCategoryRequest request, CancellationToken cancellationToken)
        {
            var Categoryid = _categoryRepository.AddCategory(request.CategoryName);
            return Categoryid;
        }
    }
}
