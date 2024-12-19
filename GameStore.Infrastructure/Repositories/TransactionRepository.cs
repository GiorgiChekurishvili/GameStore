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
    internal class TransactionRepository : ITransactionRepository
    {
        readonly GameStoreDbContext _context;
        public TransactionRepository(GameStoreDbContext context)
        {
            _context = context;
        }
        public async Task<decimal> FillBalanceByUserId(int userId, decimal balance)
        {
            var user = await _context.Users.FindAsync(userId);
            user!.Balance += balance;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user.Balance;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsByUserId(int userId)
        {
            var data = await _context.Transactions.Where(x=>x.UserId == userId).ToListAsync();
            return data;

        }

        public async Task<decimal> GetUserBalance(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user!.Balance;
        }
    }
}
