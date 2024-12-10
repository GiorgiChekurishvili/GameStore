using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.CartDTO.Validators
{
    public class CartCommandsDTOValidator : AbstractValidator<CartCommandsDTO>
    {
        public CartCommandsDTOValidator()
        {
            RuleFor(x => x.GameId).NotEmpty().WithMessage("GameId should not be empty")
                .GreaterThan(0).WithMessage("GameId must be a positive integer");
        }
    }
}
