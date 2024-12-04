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
        public int UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
