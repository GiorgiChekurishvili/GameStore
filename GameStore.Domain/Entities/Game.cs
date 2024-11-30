using GameStore.Domain.Entities.Authentication;
using GameStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    internal class Game : BaseDomainEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Price { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public int PublisherId {  get; set; }
        public User? Publisher { get; set; }
        public int DeveloperId { get; set; }
        public User? Developer { get; set; }

        public ICollection<Cart>? Carts { get; set; }
        public ICollection<GameCategory>? Categories { get; set; }
        public ICollection<Library>? Libraries { get; set; }
        public ICollection<SystemRequirement>? SystemRequirements { get; set; }
        public ICollection<Wishlist>? Wishlists { get; set; }


    }
}
