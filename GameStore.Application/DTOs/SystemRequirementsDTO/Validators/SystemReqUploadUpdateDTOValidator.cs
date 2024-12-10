using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.SystemRequirementsDTO.Validators
{
    public class SystemReqUploadUpdateDTOValidator : AbstractValidator<SystemRequirementsUploadUpdateDTO>
    {
        public SystemReqUploadUpdateDTOValidator()
        {
            RuleFor(x => x.GameId)
            .GreaterThan(0).WithMessage("Game ID must be a positive integer.");

            RuleFor(x => x.RequirementType)
            .Must(type => new[] { "Minimum", "Recommended" }.Contains(type))
            .WithMessage("Requirement Type must be either 'Minimum' or 'Recommended'.");

            RuleFor(x => x.Os)
            .MinimumLength(3).WithMessage("Operating System must be at least 3 characters long.")
            .Must(type => new[] { "Windows", "MacOs", "Linux" }.Contains(type))
            .WithMessage("Requirement Type must be either 'Windows', 'MacOs' or 'Linux'");

            RuleFor(x => x.MemoryRam)
            .Matches(@"^\d+\s*(GB|MB)$").WithMessage("Memory (RAM) must specify a size and unit (for example: '8 GB').");

            RuleFor(x => x.VideoMemoryVram)
            .Matches(@"^\d+\s*(GB|MB)$").When(x => !string.IsNullOrEmpty(x.VideoMemoryVram))
            .WithMessage("Video Memory (VRAM) must specify a size and unit (e.g., '4 GB').");

            RuleFor(x => x.DirectX)
            .Matches(@"^\d+$").When(x => !string.IsNullOrEmpty(x.DirectX))
            .WithMessage("DirectX version must be a number (e.g., '12').");

            RuleFor(x => x.Network)
            .MaximumLength(100).WithMessage("Network description must be less than 100 characters.")
            .When(x => !string.IsNullOrEmpty(x.Network));

            RuleFor(x => x.Storage)
           .NotEmpty().WithMessage("Storage is required.")
           .Matches(@"^\d+(\.\d+)?\s*(GB|MB|TB)$").WithMessage("Storage must specify a size and unit (e.g., '50 GB', '1 TB').");
        }
    }
}
