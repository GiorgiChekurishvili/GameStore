using GameStore.Domain.Entities.Authentication;
using GameStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    internal class Transaction : BaseDomainEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public decimal TransactionsMade { get; set; }
        public required string Description { get; set; }
    }
}
