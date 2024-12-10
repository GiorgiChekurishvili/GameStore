using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.WishlistDTO.Validators
{
    public class WishlistUploadDTOValidator : AbstractValidator<WishlistUploadDTO>
    {
        public WishlistUploadDTOValidator()
        {
            RuleFor(x => x.GameId).NotEmpty().WithMessage("GameId Should not be empty")
                .GreaterThan(0).WithMessage("Game Id must be a positive integer");
        }
    }
}
