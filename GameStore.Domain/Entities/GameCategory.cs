using GameStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    internal class GameCategory : BaseDomainEntity
    {
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
