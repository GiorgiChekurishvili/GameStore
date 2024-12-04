using GameStore.Application.DTOs.WishlistDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Wishlists.Requests.Commands
{
    public class AddGameToWishlistRequest : IRequest<Unit>
    {
        public WishlistUploadDTO? Wishlist {  get; set; }
    }
}
