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
    public class GameRepository : IGameRepository
    {
        readonly GameStoreDbContext _context;
        public GameRepository(GameStoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddGame(Game game)
        {
            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return game.Id;
            
        }

        public async Task DeleteGame(int gameId)
        {
            var findData = await _context.Games.FindAsync(gameId);
            if (findData != null)
            {
                _context.Games.Remove(findData);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Game>> GetAllGames()
        {
            var games = await _context.Games.ToListAsync();
            return games;
        }

        public async Task<IEnumerable<Game>> GetAllGamesByPublisherId(int UserId)
        {
            var GameByPublisher = await _context.Games.Where(x=>x.PublisherId == UserId).ToListAsync();
            return GameByPublisher;
        }

        public async Task<Game> GetGameById(int gameId)
        {
            var GameById = await _context.Games.FindAsync(gameId);
            return GameById!;
        }

        public async Task UpdateGame(Game game)
        {
            _context.Games.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
