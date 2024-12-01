using GameStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.SystemRequirementsDTO
{
    public class SystemRequirementsRetrieveDTO
    {
        public int Id { get; set; }
        public string? RequirementType { get; set; } 
        public string? Os { get; set; } 
        public string? Processor { get; set; } 
        public string? MemoryRam { get; set; } 
        public string? GraphicsCard { get; set; }
        public string VideoMemoryVram { get; set; } = string.Empty;
        public string DirectX { get; set; } = string.Empty;
        public string Network { get; set; } = string.Empty;
        public string? Storage { get; set; }
        public string AdditionalNotes { get; set; } = string.Empty;
    }
}
