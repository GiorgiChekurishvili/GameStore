using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities.Common
{
    public abstract class BaseDomainEntity
    {
        public int Id { get; set; }
    }
}
