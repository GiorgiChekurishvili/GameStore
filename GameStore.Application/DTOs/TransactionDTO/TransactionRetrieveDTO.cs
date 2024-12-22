using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.TransactionDTO
{
    public class TransactionRetrieveDTO
    {
        public int Id { get; set; }
        public decimal TransactionsMade { get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
