using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.WishlistDTO
{
    public class WishlistRetrieveDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? Publisher { get; set; }
        public string? Developer { get; set; }
        public IEnumerable<string>? Categories { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
