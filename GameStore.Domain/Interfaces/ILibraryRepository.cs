using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    internal interface ILibraryRepository
    {
        Task<IEnumerable<Library>> GeAlltLibraryGames();
        Task<IEnumerable<Library>> GetLibraryGameById(int gameId);

    }
}
