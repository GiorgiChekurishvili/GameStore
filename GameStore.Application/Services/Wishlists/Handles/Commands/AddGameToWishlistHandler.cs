﻿using AutoMapper;
using GameStore.Application.DTOs.GameDTO;
using GameStore.Application.DTOs.WishlistDTO;
using GameStore.Application.DTOs.WishlistDTO.Validators;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.Wishlists.Requests.Commands;
using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.Wishlists.Handles.Commands
{
    public class AddGameToWishlistHandler : IRequestHandler<AddGameToWishlistRequest, Unit>
    {
        readonly IWishlistRepository _wishlistRepository;
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        public AddGameToWishlistHandler(IWishlistRepository wishlistRepository, IMapper mapper, IGameRepository gameRepository)
        {
            _wishlistRepository = wishlistRepository;
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(AddGameToWishlistRequest request, CancellationToken cancellationToken)
        {
            var validator = new WishlistUploadDTOValidator();
            var validationResult = await validator.ValidateAsync(request.Wishlist!);

            if (validationResult.IsValid == false)
                throw new ValidationException(validationResult);

            var game = await _gameRepository.GetGameById(request.Wishlist!.GameId);
            if (game == null)
            {
                throw new NotFoundException("this game isnt available");
            }
            var map = _mapper.Map<Wishlist>(request.Wishlist);
            await _wishlistRepository.AddGameToWishlist(map);
            return Unit.Value;
        }
    }
}
