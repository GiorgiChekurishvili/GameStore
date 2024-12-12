using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsByUserId(int userId);
        Task<decimal> GetUserBalance(int userId);
        Task<decimal> FillBalanceByUserId(int userId, decimal balance);
        
    }
}
