using AutoMapper;
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
    public class AddGameToCartHandler : IRequestHandler<AddGameToCartRequest, Unit>
    {
        readonly ICartRepository _cartRepository;
        readonly ILibraryRepository _libraryRepository;
        readonly IMapper _mapper;
        public AddGameToCartHandler(ICartRepository cartRepository, IMapper mapper, ILibraryRepository libraryRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _libraryRepository = libraryRepository;
        }

        public async Task<Unit> Handle(AddGameToCartRequest request, CancellationToken cancellationToken)
        {
            var validator = new CartCommandsDTOValidator();
            var validationResult = await validator.ValidateAsync(request.CartDTO!);
            if (validationResult.IsValid == false)
            {
                throw new ValidationException(validationResult);
            }
            var cartGames = await _cartRepository.GetCartGames(request.CartDTO!.UserId);
            var mapCartGames = _mapper.Map<IEnumerable<Cart>>(cartGames);
            var LibraryGames = await _libraryRepository.GetLibraryGameById(request.CartDTO.GameId, request.CartDTO.UserId);
            if (LibraryGames != null)
            {
                throw new BadRequestException("Game already exists in the library.");
            }
            if (mapCartGames.Any(cartGame => cartGame.GameId == request.CartDTO.GameId))
            {
                throw new BadRequestException("Game already exists in the cart.");
            }
            var map = _mapper.Map<Cart>(request.CartDTO);
            await _cartRepository.AddGameToCart(map);
            return Unit.Value;
        }
    }
}
