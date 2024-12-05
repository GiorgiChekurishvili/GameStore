using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Repositories
{
    internal class SystemRequirementsRepository : ISystemRequirementsRepository
    {
        public Task<int> AddSystemRequirements(SystemRequirement systemRequirement)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SystemRequirement>> GetSystemRequirementsForGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateSystemRequirements(SystemRequirement systemRequirement)
        {
            throw new NotImplementedException();
        }
    }
}
