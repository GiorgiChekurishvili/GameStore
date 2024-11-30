using GameStore.Domain.Entities.Authentication;
using GameStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class Wishlist : BaseDomainEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int GamedId { get; set; }
        public Game? Game { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
