using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.CartDTO
{
    public class CartCommandsDTO
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
    }
}
