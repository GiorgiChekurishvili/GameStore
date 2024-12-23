using AutoMapper;
using GameStore.Application.Services.Transactions.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Transactions.Handles.Queries
{
    public class GetUserBalanceHandler : IRequestHandler<GetUserBalanceRequest, decimal>
    {
        readonly ITransactionRepository _transactionRepository;
        public GetUserBalanceHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<decimal> Handle(GetUserBalanceRequest request, CancellationToken cancellationToken)
        {
            var data = await _transactionRepository.GetUserBalance(request.UserId);
            return data;
        }
    }
}
