using System;
using System.Linq;

namespace GameStore.Application.DTOs.GameDTO
{
    public class GamesRetrieveDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int PublisherId { get; set; }
        public string? Publisher { get; set; }
        public int DeveloperId { get; set; }
        public string? Developer { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<string>? Categories { get; set; }

    }
}
