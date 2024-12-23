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
    public class CartRepository : ICartRepository
    {
        readonly GameStoreDbContext _context;
        public CartRepository(GameStoreDbContext context)
        {
            _context = context;
        }
        public async Task AddGameToCart(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public async Task CheckoutGames(int userId)
        {
            var data = await _context.Carts
                .Include(x=>x.Game).Where(x=>x.UserId == userId).ToListAsync();

            
            foreach (var item in data)
            {
                Library library = new Library { GameId = item.GameId, UserId = item.UserId };
                await _context.Libraries.AddAsync(library);
                _context.Carts.Remove(item);


            }
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Balance -= data.Sum(x => x.Game!.Price);
            }
            var transaction = new Transaction
            {
                UserId = userId,
                TransactionsMade = data.Sum(x => x.Game!.Price),
                Description = $"Purchased {data.Count} games"
            };
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();

            
        }

        public async Task<IEnumerable<Cart>> GetCartGames(int userId)
        {
            var data = await _context.Carts.Include(x=>x.Game).Where(x=>x.UserId == userId).ToListAsync();
            return data;
        }

        public async Task RemoveAllGamesFromCart(int userId)
        {
            var data = await _context.Carts.Where(x => x.UserId == userId).ToListAsync();
            if (data != null)
            {
                foreach (var item in data)
                {
                    _context.Carts.Remove(item);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveGameFromCart(Cart cart)
        {
            var data = await _context.Carts.Where(x=> x.UserId == cart.UserId && x.GameId == cart.GameId).FirstOrDefaultAsync();
            _context.Remove(data!);
            await _context.SaveChangesAsync();
        }
        public async Task<decimal> TotalPriceOfCartGames(int userId)
        {
            decimal totalPrice = 0;
            var cartItems = await _context.Carts.Include(x=>x.Game).Where(x => x.UserId == userId).ToListAsync();
            foreach (var item in cartItems)
            {
                totalPrice += item.Game!.Price;
            }
            return totalPrice;
        }
    }
}
