using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TechnicalExam.Services.Models
{
    public class AccountsModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public double InitialBalance { get; set; }
    }

    public class AccountModelValidator : AbstractValidator<AccountsModel>
    {
        public AccountModelValidator()
        {
            RuleFor(p => p.Username).NotNull().NotEmpty().WithMessage("Username cannot be empty!");
            RuleFor(p => p.InitialBalance).NotNull().NotEqual(0).WithMessage("Initial Balance is required and must be greater than 0!");
        }
    }
}
