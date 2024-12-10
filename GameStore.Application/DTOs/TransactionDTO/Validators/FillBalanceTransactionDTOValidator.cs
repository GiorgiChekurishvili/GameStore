using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Application.DTOs.TransactionDTO.Validators
{
    public class FillBalanceTransactionDTOValidator : AbstractValidator<FIllBalanceTransactionDTO>
    {
        public FillBalanceTransactionDTOValidator()
        {
            RuleFor(x => x.Balance).NotEmpty().WithMessage("Balance is required to be filled")
                .GreaterThan(0).WithMessage("input must be a positive number");
        }
    }
}
