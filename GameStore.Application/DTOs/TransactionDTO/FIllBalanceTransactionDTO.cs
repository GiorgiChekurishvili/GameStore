using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.TransactionDTO
{
    public class FIllBalanceTransactionDTO
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
