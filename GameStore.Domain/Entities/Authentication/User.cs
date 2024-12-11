using GameStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities.Authentication
{
    public class User : BaseDomainEntity
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public decimal Balance { get; set; }
        public DateTime UserCreated { get; set; } = DateTime.Now;

        public ICollection<UserRole>? Roles { get; set; }
        public ICollection<Cart>? Carts { get; set; }
        public ICollection<Game>? Publishers {  get; set; }
        public ICollection<Game>? Developers { get; set; }
        public ICollection<Library>? Libraries { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
        public ICollection<Wishlist>? Wishlists { get; set; }
    }
}
