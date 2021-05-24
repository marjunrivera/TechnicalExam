using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalExam.Services.Models
{
    public class TransactionsModel
    {
        public int Id { get; set; }
        public int SourceAccountId { get; set; }
        public int DestinationAccountId { get; set; }
        public double TransferAmount { get; set; }
    }

    public class TransactionsModelValidator : AbstractValidator<TransactionsModel>
    {
        public TransactionsModelValidator()
        {
            RuleFor(p => p.SourceAccountId).NotNull().NotEmpty().WithMessage("SourceAccountId is required.");
            RuleFor(p => p.DestinationAccountId).NotEqual(0).WithMessage("DestinationAccountId is required.");
            RuleFor(p => p.TransferAmount).NotNull().NotEqual(0).WithMessage("TransferAmount is required and must not be equal to 0.");
        }
    }
}
