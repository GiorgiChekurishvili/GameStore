using GameStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GameStore.Infrastructure.Repositories
{
    internal class TransactionRepository : ITransactionRepository
    {
        public Task<decimal> FillBalanceByUserId(int userId, decimal balance)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Transaction>> GetAllTransactionsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetUserBalance(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
