using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.SystemRequirementsDTO
{
    public class SystemRequirementsUploadUpdateDTO
    {
        [Required]
        public int GameId { get; set; }
        [Required]
        public string? RequirementType { get; set; }
        [Required]
        public string? Os { get; set; }
        [Required]
        public string? Processor { get; set; }
        [Required]
        public string? MemoryRam { get; set; }
        [Required]
        public string? GraphicsCard { get; set; }
        public string? VideoMemoryVram { get; set; } 
        public string? DirectX { get; set; }
        public string? Network { get; set; }
        [Required]
        public string? Storage { get; set; }
        public string? AdditionalNotes { get; set; }
    }
}

