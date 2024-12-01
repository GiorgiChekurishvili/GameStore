using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GameStore.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsByUserId(int userId);
        Task<decimal> GetUserBalance(int userId);
        Task<decimal> FillBalanceForUser(int userId, decimal balance);
        
    }
}
