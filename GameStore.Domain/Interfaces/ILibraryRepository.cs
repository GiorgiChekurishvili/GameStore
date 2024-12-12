using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    public interface ILibraryRepository
    {
        Task<IEnumerable<Library>> GeAllLibraryGames();
        Task<Library> GetLibraryGameById(int gameId);

    }
}
