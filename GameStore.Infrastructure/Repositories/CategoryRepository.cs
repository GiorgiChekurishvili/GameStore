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
                _context.Categories.Remove(data);
            
        }

        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            var data = await _context.Categories.ToListAsync();
            return data;
        }

        public async Task<IEnumerable<Category>> GetAllGamesByCategory(int categoryId)
        {
            var GamesByCategory = await _context.Categories.Include(x=>x.Games)!
                .ThenInclude(x=>x.Game).Where(x=>x.Id == categoryId).ToListAsync();
            return GamesByCategory;
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Categories.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
