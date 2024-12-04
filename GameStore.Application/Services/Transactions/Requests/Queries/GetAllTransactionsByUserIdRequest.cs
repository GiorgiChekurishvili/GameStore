using GameStore.Application.DTOs.TransactionDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Transactions.Requests.Queries
{
    public class GetAllTransactionsByUserIdRequest : IRequest<TransactionRetrieveDTO>
    {
        public int UserId { get; set; }
    }
}
