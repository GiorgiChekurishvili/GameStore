using GameStore.Application.DTOs.WishlistDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Wishlists.Requests.Queries
{
    public class GetWishlistGamesRequest : IRequest<IEnumerable<WishlistRetrieveDTO>>
    {
        public int UserId { get; set; }
    }
}
