using GameStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities
{
    public class SystemRequirement : BaseDomainEntity
    {
        public int GameId { get; set; }
        public Game? Game { get; set; }
        public required string RequirementType { get; set; }
        public required string Os {  get; set; }
        public required string Processor { get; set; }
        public required string MemoryRam { get; set; }
        public required string GraphicsCard { get; set; }
        public string? DirectX { get; set; }
        public string? Network { get; set; }
        public required string Storage { get; set; }
        public string? AdditionalNotes { get; set; }
    }
}
