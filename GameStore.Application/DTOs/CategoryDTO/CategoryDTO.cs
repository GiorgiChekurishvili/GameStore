using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.CategoryDTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
    }
}
