using GameStore.Domain.Entities.Authentication;
using GameStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class Library : BaseDomainEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}
