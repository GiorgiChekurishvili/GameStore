using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.LibraryDTO
{
    public class LibraryRetrieveDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Now;
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
    }
}
