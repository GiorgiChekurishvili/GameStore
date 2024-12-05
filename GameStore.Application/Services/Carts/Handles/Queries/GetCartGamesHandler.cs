using AutoMapper;
using GameStore.Application.DTOs.CartDTO;
using GameStore.Application.Services.Carts.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Carts.Handles.Queries
{
    public class GetCartGamesHandler : IRequestHandler<GetCartGamesRequest, IEnumerable<CartRetrieveDTO>>
    {
        readonly ICartRepository _cartRepository;
        readonly IMapper _mapper;
        public GetCartGamesHandler(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CartRetrieveDTO>> Handle(GetCartGamesRequest request, CancellationToken cancellationToken)
        {
            var data = await _cartRepository.GetCartGames(request.UserId);
            var map = _mapper.Map<IEnumerable<CartRetrieveDTO>>(data);
            return map;
        }
    }
}
