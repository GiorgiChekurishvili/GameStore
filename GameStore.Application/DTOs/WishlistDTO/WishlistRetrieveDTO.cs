using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.WishlistDTO
{
    public class WishlistRetrieveDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int PublisherId { get; set; }
        public int DeveloperId { get; set; }
        public DateTime AddedOn { get; set; }
    }
}
