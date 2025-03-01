﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.GameDTO
{
    public class GameUploadUpdateDTO
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int DeveloperId { get; set; }
        [Required]
        public List<int>? CategoryIds { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
