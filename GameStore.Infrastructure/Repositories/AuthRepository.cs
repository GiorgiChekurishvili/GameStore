using GameStore.Domain.Entities.Authentication;
using GameStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Repositories
{
    internal class AuthRepository : IAuthRepository
    {
        readonly GameStoreDbContext _context;
        public AuthRepository(GameStoreDbContext context)
        {
            _context = context;
        }

        public async Task<User?> Login(User user)
        {
            var data = await _context.Users.Include(x=>x.Role).Where(x=>x.UserName == user.UserName).FirstOrDefaultAsync();
            return data;
        }

        public async Task Register(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
    }
}
