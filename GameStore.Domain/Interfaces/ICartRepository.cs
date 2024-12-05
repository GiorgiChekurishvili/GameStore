using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task AddGameToCart(Cart cart);
        Task<IEnumerable<Cart>> GetCartGames(int userId);
        Task CheckoutGames(int userId);
        Task RemoveGameFromCart(Cart cart);
        Task RemoveAllGamesFromCart(int userId);

    }
}
