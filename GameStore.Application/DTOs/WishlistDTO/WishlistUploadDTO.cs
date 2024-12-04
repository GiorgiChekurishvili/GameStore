using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.WishlistDTO
{
    public class WishlistUploadDTO
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
    }
}
