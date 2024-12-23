using AutoMapper;
using GameStore.Application.DTOs.TransactionDTO;
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
    public class GetAllTransactionsByUserIdHandler : IRequestHandler<GetAllTransactionsByUserIdRequest, IEnumerable<TransactionRetrieveDTO>>
    {
        readonly ITransactionRepository _transactionRepository;
        readonly IMapper _mapper;
        readonly IDistributedCache _cache;
        public GetAllTransactionsByUserIdHandler(ITransactionRepository transactionRepository, IMapper mapper, IDistributedCache cache)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _cache = cache;
        }
        public async Task<IEnumerable<TransactionRetrieveDTO>> Handle(GetAllTransactionsByUserIdRequest request, CancellationToken cancellationToken)
        {
            var cachekey = $"GetAllTransactionsByUserId-{request.UserId}";
            var cacheData = await _cache.GetStringAsync(cachekey);
            if (!string.IsNullOrEmpty(cacheData))
            {
                return JsonConvert.DeserializeObject<IEnumerable<TransactionRetrieveDTO>>(cacheData)!;
            }
            var data = await _transactionRepository.GetAllTransactionsByUserId(request.UserId);
            var map = _mapper.Map<IEnumerable<TransactionRetrieveDTO>>(data);
            var cacheOptions = new DistributedCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
            await _cache.SetStringAsync(cachekey, JsonConvert.SerializeObject(map), cacheOptions);
            return map;
        }
    }
}
