using AutoMapper;
using GameStore.Application.Cache;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Carts.Requests.Commands;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Carts.Handles.Commands
{
    public class CheckoutGamesHandler : IRequestHandler<CheckoutGamesRequest, Unit>
    {
        readonly ICartRepository _cartRepository;
        readonly ITransactionRepository _transactionRepository;
        readonly ICacheService _cacheService;
        public CheckoutGamesHandler(ICartRepository cartRepository, ITransactionRepository transactionRepository, ICacheService cacheService)
        {
            _cartRepository = cartRepository;
            _transactionRepository = transactionRepository;
            _cacheService = cacheService;
        }
        public async Task<Unit> Handle(CheckoutGamesRequest request, CancellationToken cancellationToken)
        {
            var userBalance = await _transactionRepository.GetUserBalance(request.UserId);
            var totalPrice = await _cartRepository.TotalPriceOfCartGames(request.UserId);
            if (userBalance < totalPrice)
            {
                throw new BadRequestException("Insufficient balance to complete the purchase.");
            }
            await _cartRepository.CheckoutGames(request.UserId);
            await _cacheService.RemoveCache("GetCartGames", request.UserId);
            await _cacheService.RemoveCache("GetAllLibraryGames", request.UserId);
            await _cacheService.RemoveCache("GetAllTransactionsByUserId", request.UserId);
            await _cacheService.RemoveCache("GetUserBalance", request.UserId);      
            return Unit.Value;
        }
    }
}
