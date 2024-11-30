using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    internal interface IWishlistRepository
    {
        Task AddGameToWishlistAsynt(Wishlist wishlist);
        Task RemoveGameFromWishlist(Wishlist wishlist);
        Task<IEnumerable<Game>> GetWishlistGames(int userId);
        
    }
}
