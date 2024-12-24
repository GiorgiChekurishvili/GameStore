using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        readonly GameStoreDbContext _context;
        public CategoryRepository(GameStoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task DeleteCategory(int categoryId)
        {
            var data = await _context.Categories.FindAsync(categoryId);
            if (data != null)
            {
                _context.Categories.Remove(data);
                await _context.SaveChangesAsync();
            }


            
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var data = await _context.Categories.ToListAsync();
            return data;
        }

        public async Task<Category> GetCategoryById(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            return category!;
        }

        public async Task UpdateCategory(Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(category.Id);
            if (existingCategory != null)
            {
                _context.Entry(existingCategory).State = EntityState.Detached;
            }
            _context.Categories.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
