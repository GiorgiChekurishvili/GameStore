﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    public interface IGameCategoryRepository
    {
        Task UpdateGameCategory(List<int> ids, int gameId);
    }
}
