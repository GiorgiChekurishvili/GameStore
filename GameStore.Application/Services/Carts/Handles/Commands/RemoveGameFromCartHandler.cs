using AutoMapper;
using GameStore.Application.Cache;
using GameStore.Application.DTOs.CartDTO;
using GameStore.Application.DTOs.CartDTO.Validators;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Carts.Requests.Commands;
using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Carts.Handles.Commands
{
    public class RemoveGameFromCartHandler : IRequestHandler<RemoveGameFromCartRequest, Unit>
    {
        readonly ICartRepository _cartRepository;
        readonly IMapper _mapper;
        readonly ICacheService _cacheService; 
        public RemoveGameFromCartHandler(ICartRepository cartRepository, IMapper mapper, ICacheService cacheService)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        public async Task<Unit> Handle(RemoveGameFromCartRequest request, CancellationToken cancellationToken)
        {
            var validator = new CartCommandsDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CartDTO!);

            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }

            var map = _mapper.Map<Cart>(request.CartDTO);
            await _cartRepository.RemoveGameFromCart(map);
            await _cacheService.RemoveCache("GetCartGames", request.CartDTO!.UserId);
            return Unit.Value;
        }
    }
}
