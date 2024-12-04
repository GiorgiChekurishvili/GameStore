using AutoMapper;
using GameStore.Application.DTOs.TransactionDTO;
using GameStore.Application.Services.Transactions.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Transactions.Handles.Queries
{
    public class GetAllTransactionsByUserIdHandler : IRequestHandler<GetAllTransactionsByUserIdRequest, TransactionRetrieveDTO>
    {
        readonly ITransactionRepository _transactionRepository;
        readonly IMapper _mapper;
        public GetAllTransactionsByUserIdHandler(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
        }
        public async Task<TransactionRetrieveDTO> Handle(GetAllTransactionsByUserIdRequest request, CancellationToken cancellationToken)
        {
            var data = await _transactionRepository.GetAllTransactionsByUserId(request.UserId);
            var map = _mapper.Map<TransactionRetrieveDTO>(data);
            return map;
        }
    }
}
