using AutoMapper;
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
        public RemoveGameFromCartHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(RemoveGameFromCartRequest request, CancellationToken cancellationToken)
        {
            var map = _mapper.Map<Cart>(request.CartDTO);
            await _cartRepository.RemoveGameFromCart(map);
            return Unit.Value;
        }
    }
}
