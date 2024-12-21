using System;
using System.Linq;

namespace GameStore.Application.DTOs.LibraryDTO
{
    public class LibraryRetrieveDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime PurchaseDate { get; set; }
        public IEnumerable<string>? Categories { get; set; }
    }
}
