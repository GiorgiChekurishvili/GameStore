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
    internal class WishlistRepository : IWishlistRepository
    {
        readonly GameStoreDbContext _context;
        public WishlistRepository(GameStoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddGameToWishlist(Wishlist wishlist)
        {
            await _context.Wishlist.AddAsync(wishlist);
            await _context.SaveChangesAsync();
            return wishlist.Id;
        }

        public async Task<IEnumerable<Wishlist>> GetWishlistGames(int userId)
        {
            var wishlistGames = await _context.Wishlist.Include(x=>x.Game).Where(x=>x.UserId == userId).ToListAsync();
            return wishlistGames;
        }

        public async Task RemoveGameFromWishlist(Wishlist wishlist)
        {
            var game = await _context.Wishlist.Where(x=>x.UserId == wishlist.UserId && x.GameId ==  wishlist.GameId).FirstOrDefaultAsync();
            if (game != null)
            {
                _context.Wishlist.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
    }
}
