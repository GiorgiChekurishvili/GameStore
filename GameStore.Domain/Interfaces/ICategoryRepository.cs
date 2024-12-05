using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<int> AddCategory(string CategoryName);
        Task UpdateCategory(Category category);
        Task DeleteCategory(int categoryId);
    }
}
