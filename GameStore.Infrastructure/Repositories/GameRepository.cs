using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Repositories
{
    internal class GameRepository : IGameRepository
    {
        public Task<int> AddGame(Game game)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAllGames()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetAllGamesByPublisherId(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetGameById(int gameId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGame(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
