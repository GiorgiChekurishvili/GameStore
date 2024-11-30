using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    internal interface ISystemRequirementsRepository
    {
        Task AddSystemRequirements(SystemRequirement systemRequirement);
        Task UpdateSystemRequirements(SystemRequirement systemRequirement);
        Task<SystemRequirement> GetSystemRequirementsForGame(int gameId);
    }
}
