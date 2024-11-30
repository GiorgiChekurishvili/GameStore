using GameStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    internal class Category : BaseDomainEntity
    {
        public required string CategoryName { get; set; }

        public ICollection<GameCategory>? Games { get; set; }
    }
}
