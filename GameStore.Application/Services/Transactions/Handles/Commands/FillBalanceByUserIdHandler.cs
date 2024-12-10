using GameStore.Application.DTOs.TransactionDTO.Validators;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Transactions.Requests.Commands;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Transactions.Handles.Commands
{
    internal class FillBalanceByUserIdHandler : IRequestHandler<FillBalanceByUserIdRequest, decimal>
    {
        readonly ITransactionRepository _transactionRepository;
        public FillBalanceByUserIdHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<decimal> Handle(FillBalanceByUserIdRequest request, CancellationToken cancellationToken)
        {
            var validator = new FillBalanceTransactionDTOValidator();
            var validationResult = await validator.ValidateAsync(request.FillBalance);
            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);
            var data = await _transactionRepository.FillBalanceByUserId(request.FillBalance.UserId, request.FillBalance.Balance);
            return data;
        }
    }
}
