﻿using GameStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities.Authentication
{
    public class Role : BaseDomainEntity
    {
        public required string RoleName { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
