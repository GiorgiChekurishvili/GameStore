using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Repositories
{
    internal class CartRepository : ICartRepository
    {
        public Task AddGameToCart(Cart cart)
        {
            throw new NotImplementedException();
        }

        public Task CheckoutGames(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Cart>> GetCartGames(int userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAllGamesFromCart(int userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveGameFromCart(Cart cart)
        {
            throw new NotImplementedException();
        }
    }
}
