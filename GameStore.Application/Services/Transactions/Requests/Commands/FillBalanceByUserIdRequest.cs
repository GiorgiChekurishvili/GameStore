using GameStore.Application.DTOs.TransactionDTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Transactions.Requests.Commands
{
    public class FillBalanceByUserIdRequest : IRequest<decimal>
    {
        public required FIllBalanceTransactionDTO FillBalance {  get; set; }
    }
}
