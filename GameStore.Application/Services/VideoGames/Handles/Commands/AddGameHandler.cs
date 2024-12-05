﻿using AutoMapper;
using GameStore.Application.Services.VideoGames.Requests.Commands;
using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.VideoGames.Handles.Commands
{
    public class AddGameHandler : IRequestHandler<AddGameRequest, int>
    {
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        public AddGameHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
        }
        public async Task<int> Handle(AddGameRequest request, CancellationToken cancellationToken)
        {
            var game = _mapper.Map<Game>(request.GameUploadDTO);
            var id = await _gameRepository.AddGame(game);
            return id;
        }
    }
}