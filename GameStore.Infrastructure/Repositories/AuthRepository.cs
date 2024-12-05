using GameStore.Domain.Entities.Authentication;
using GameStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Infrastructure.Repositories
{
    internal class AuthRepository : IAuthRepository
    {
        public Task<User> CheckUserByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<string> Login(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
