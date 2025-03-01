﻿using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

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
            var games = await _context.Games
                .Include(x => x.Categories)!.ThenInclude(x => x.Category)
                .Include(x=>x.Publisher)
                .Include(x=>x.Developer).ToListAsync();
            return games;
        }

        public async Task<IEnumerable<Game>> GetAllGamesByPublisherId(int UserId)
        {
            var GameByPublisher = await _context.Games
                .Include(x => x.Categories)!.ThenInclude(x => x.Category)
                .Include(x => x.Publisher)
                .Include(x => x.Developer).Where(x => x.PublisherId == UserId).ToListAsync();
            return GameByPublisher;
        }
        public async Task<IEnumerable<Game>> GetAllGamesByCategory(int categoryId)
        {
            var GamesByCategory = await _context.Games
                .Include(x => x.Categories)!
                .ThenInclude(x => x.Category)
                .Include(x => x.Publisher)
                .Include(x => x.Developer)
                .Where(x => x.Categories!.Any(x => x.CategoryId == categoryId)).ToListAsync();
            return GamesByCategory;
        }
        public async Task<Game> GetGameById(int gameId)
        {
            var GameById = await _context.Games
                .Include(x => x.Categories)!.ThenInclude(x => x.Category)
                .Include(x=>x.Publisher)
                .Include(x => x.Developer).FirstOrDefaultAsync(x => x.Id == gameId);
            return GameById!;
        }

        public async Task UpdateGame(Game game)
        {
            _context.Games.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
