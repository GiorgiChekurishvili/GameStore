﻿using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    internal interface IGameRepository
    {
        Task<IEnumerable<Game>> GetAllGames();
        Task<IEnumerable<Game>> GetAllGamesByPublisherId(int UserId);
        Task<IEnumerable<Game>> GetGameById(int gameId);
        Task<int> AddGame(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(int gameId);

    }
}
