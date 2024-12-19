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
    public class GameCategoryRepository : IGameCategoryRepository
    {
        readonly GameStoreDbContext _context;
        public GameCategoryRepository(GameStoreDbContext context)
        {
            _context = context;
        }
        public async Task UpdateGameCategory(List<int> ids, int gameId)
        {
            var gameRetrieve = await _context.GameCategories.Where(x => x.GameId == gameId).ToListAsync();
            
            var categoriesToAdd = ids.Except(gameRetrieve.Select(x=>x.CategoryId)).ToList();
            var categoriesToRemove = gameRetrieve.Where(x=> !ids.Contains(x.CategoryId)).ToList();

            foreach ( var categoryId in categoriesToAdd )
            {
                var gameCategory = new GameCategory
                {
                    GameId = gameId,
                    CategoryId = categoryId
                };
                _context.GameCategories.Add(gameCategory);
            }
            foreach (var category in categoriesToRemove)
            {
                _context.GameCategories.Remove(category);
            }
            await _context.SaveChangesAsync();

        }
    }
}
