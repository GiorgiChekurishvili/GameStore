using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Repositories
{
    internal class SystemRequirementsRepository : ISystemRequirementsRepository
    {
        readonly GameStoreDbContext _context;
        public SystemRequirementsRepository(GameStoreDbContext context)
        {
            _context = context;
        }
        public async Task<int> AddSystemRequirements(SystemRequirement systemRequirement)
        {
            await _context.SystemRequirements.AddAsync(systemRequirement);
            await _context.SaveChangesAsync();
            return systemRequirement.Id;
        }

        public async Task<IEnumerable<SystemRequirement>> GetSystemRequirementsForGame(int gameId)
        {
            var data = await _context.SystemRequirements.Include(x=>x.Game).Where(x=>x.GameId == gameId).ToListAsync();
            return data;
        }

        public async Task UpdateSystemRequirements(SystemRequirement systemRequirement)
        {
            _context.SystemRequirements.Entry(systemRequirement).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
