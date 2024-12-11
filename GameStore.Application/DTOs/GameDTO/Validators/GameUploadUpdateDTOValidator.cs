using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.GameDTO.Validators
{
    public class GameUploadUpdateDTOValidator : AbstractValidator<GameUploadUpdateDTO>
    {
        public GameUploadUpdateDTOValidator()
        {
            RuleFor(x => x.Name).MaximumLength(100)
                .WithMessage("Name must not exceed 100 characters");
            RuleFor(x => x.Description).MaximumLength(500)
                .WithMessage("Description Must not exceed 500 characters");
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be a positive number or free (e.g 0)");
            RuleFor(x => x.DeveloperId)
                .GreaterThan(0).WithMessage("Developer ID must be a positive integer.");
            RuleFor(x => x.CategoryIds)
                .NotEmpty().WithMessage("Category IDs cannot be empty.")
                .Must(HaveUniqueIds!).WithMessage("Category IDs must be unique.");
        }
        private bool HaveUniqueIds(List<int> categoryIds)
        {
            return categoryIds.Distinct().Count() == categoryIds.Count;
        }
    }
}
