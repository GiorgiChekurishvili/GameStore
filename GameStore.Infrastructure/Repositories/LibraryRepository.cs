using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Repositories
{
    internal class LibraryRepository : ILibraryRepository
    {
        public Task<IEnumerable<Library>> GeAllLibraryGames()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Library>> GetLibraryGameById(int gameId)
        {
            throw new NotImplementedException();
        }
    }
}
