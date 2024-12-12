using GameStore.Domain.Entities;
using GameStore.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Repositories
{
    internal class LibraryRepository : ILibraryRepository
    {
        readonly GameStoreDbContext _context;
        public LibraryRepository(GameStoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Library>> GeAllLibraryGames()
        {
            var libraryGames = await _context.Libraries.Include(x => x.Game).ToListAsync();
            return libraryGames;
        }

        public async Task<Library> GetLibraryGameById(int gameId)
        {
            var libraryGame = await _context.Libraries.Include(x=>x.Game).Where(x=>x.GameId ==  gameId).FirstOrDefaultAsync();
            return libraryGame!;
        }
    }
}
