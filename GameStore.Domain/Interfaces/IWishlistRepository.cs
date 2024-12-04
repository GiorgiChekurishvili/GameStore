using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    public interface IWishlistRepository
    {
        Task AddGameToWishlist(Wishlist wishlist);
        Task RemoveGameFromWishlist(Wishlist wishlist);
        Task<IEnumerable<Game>> GetWishlistGames(int userId);
        
    }
}
