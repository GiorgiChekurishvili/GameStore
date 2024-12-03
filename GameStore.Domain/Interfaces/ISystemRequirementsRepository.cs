using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    public interface ISystemRequirementsRepository
    {
        Task<int> AddSystemRequirements(SystemRequirement systemRequirement);
        Task UpdateSystemRequirements(SystemRequirement systemRequirement);
        Task<IEnumerable<SystemRequirement>> GetSystemRequirementsForGame(int gameId);
    }
}
