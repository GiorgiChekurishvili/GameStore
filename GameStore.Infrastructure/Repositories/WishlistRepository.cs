using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Repositories
{
    internal class WishlistRepository : IWishlistRepository
    {
        public Task AddGameToWishlist(Wishlist wishlist)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetWishlistGames(int userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveGameFromWishlist(Wishlist wishlist)
        {
            throw new NotImplementedException();
        }
    }
}
