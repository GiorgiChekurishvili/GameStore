using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    internal interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<IEnumerable<Game>> GetAllGamesByCategory(int categoryId);
        Task<int> AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int categoryId);
    }
}
