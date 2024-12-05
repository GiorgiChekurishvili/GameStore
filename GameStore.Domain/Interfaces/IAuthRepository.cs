using GameStore.Domain.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Interfaces
{
    public interface IAuthRepository
    {
        Task<string> Login(User user);
        Task<User> Register(User user);
        Task<User> CheckUserByUserName(string userName);
    }
}
