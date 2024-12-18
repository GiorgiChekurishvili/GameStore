﻿using AutoMapper;
using GameStore.Application.DTOs.GameDTO;
using GameStore.Application.Exceptions;
using GameStore.Application.Services.VideoGames.Requests.Queries;
using GameStore.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.Services.VideoGames.Handles.Queries
{
    public class GetAllGamesByPublisherIdHandler : IRequestHandler<GetAllGamesByPublisherIdRequest, IEnumerable<GamesRetrieveDTO>>
    {
        readonly IGameRepository _gameRepository;
        readonly IMapper _mapper;
        public GetAllGamesByPublisherIdHandler(IGameRepository gameRepository, IMapper mapper)
        {
            _mapper = mapper;
            _gameRepository = gameRepository;
        }
        public async Task<IEnumerable<GamesRetrieveDTO>> Handle(GetAllGamesByPublisherIdRequest request, CancellationToken cancellationToken)
        {
            var data = await _gameRepository.GetAllGamesByPublisherId(request.UserId);
            if (data == null)
            {
                throw new NotFoundException("Game Not Found");
            }
            var map = _mapper.Map<IEnumerable<GamesRetrieveDTO>>(data);
            return map;
        }
    }
}
